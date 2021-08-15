using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.DirectX.Internal
{
	//https://docs.microsoft.com/de-de/dotnet/standard/native-interop/type-marshaling
	//https://docs.microsoft.com/en-us/previous-versions/ms808940(v=msdn.10)
	//https://docs.microsoft.com/en-us/previous-versions/ms811324(v=msdn.10)
	//https://docs.microsoft.com/en-us/previous-versions/ms811324(v=msdn.10)?source=docs

	internal static class DirectMusicImport
    {
		private static readonly Guid CLSID_DirectMusicPerformance = new Guid("d2ac2881-b39b-11d1-8704-00600893b1bd");
		private static readonly Guid CLSID_DirectMusicLoader      = new Guid("d2ac2892-b39b-11d1-8704-00600893b1bd");
		private static readonly Guid CLSID_DirectMusic            = new Guid("636b9f10-0c7d-11d1-95b2-0020afdc7421");
		
		private static IDirectMusic directMusic;
		private static IDirectMusicLoader8 directMusicLoader8;
		//IDirectMusicPerformance8 performance;
		//IDirectMusicSegment8 pSegment;
		//IDirectMusicPort8 pMusicPort;
		//IDirectMusicSegmentState pSegmentState;
		//IDirectMusicSegmentState8 pSegmentState8;
		//IDirectMusicAudioPath8 p3DAudioPath;
		//IDirectSound3DBuffer8 pDSB;

		private const int S_OK = 0; 

		internal static readonly int DMUS_PORTCAPSSize = Marshal.SizeOf(typeof(DMUS_PORTCAPS));

		static DirectMusicImport()
		{
			//comType = Type.GetTypeFromCLSID(guid);

			//bool isCOM = comType.IsCOMObject;

			//var typeDirectMusicLoader = Type.GetTypeFromCLSID(CLSID_DirectMusicLoader);
			//object obj = Activator.CreateInstance(typeDirectMusicLoader);
			//directMusicLoader8 = (IDirectMusicLoader8)obj;

			var typeDirectMusic = Type.GetTypeFromCLSID(CLSID_DirectMusic);
			//var typeDirectMusic2 = Type.GetTypeFromProgID("Microsoft.DirectMusic", true);
			directMusic = (IDirectMusic)Activator.CreateInstance(typeDirectMusic);

			//DMUS_PORTCAPS portcaps = new();
			//portcaps.dwSize = DMUS_PORTCAPSSize;
			//directMusic.EnumPort(0, ref portcaps);
			////if (FAILED(hr = CoCreateInstance(CLSID_DirectMusicLoader, NULL, CLSCTX_INPROC, IID_IDirectMusicLoader8, (void**)&m_pLoader))) return hr;
			//loader = new DirectMusicLoader();

			////if (FAILED(hr = CoCreateInstance(CLSID_DirectMusicPerformance, NULL, CLSCTX_INPROC, IID_IDirectMusicPerformance8, (void**)&m_pPerformance))) return hr;
			//this.performance = DirectMusicPerformance();

			//performance.Init(out this.music, IntPtr.Zero, IntPtr.Zero);
			//this.music8 = this.music;
		}

		public static void AddInDevices(SoundDeviceType soundDeviceType, List<MidiInDevice> devices)
        {
			DMUS_PORTCAPS portcaps = new();
			portcaps.Size = DMUS_PORTCAPSSize;
			
			int index = 0;
			while (directMusic.EnumPort(index++, ref portcaps) == S_OK && portcaps.GuidPort != Guid.Empty)
            {
				if (portcaps.Class == DMusClass.Input)
				{
					if ((soundDeviceType.HasFlag(SoundDeviceType.DirectX_EMU) && portcaps.Type == DMusType.WINMM_DRIVER) ||
						(soundDeviceType.HasFlag(SoundDeviceType.DirectX_ORG) && portcaps.Type != DMusType.WINMM_DRIVER))
					{
						Debug.WriteLine($"{index} {portcaps.GuidPort} {portcaps.Description}");
						devices.Add(new MidiInDirectXDevice(portcaps.GuidPort, portcaps.Description, $"{portcaps.Class} - {portcaps.Type} - {portcaps.Flags} - {portcaps.EffectFlags}"));
					}
				}
				portcaps.GuidPort = Guid.Empty;
			}
        }

		public static void AddOutDevices(SoundDeviceType soundDeviceType, List<MidiOutDevice> devices)
		{
			DMUS_PORTCAPS portcaps = new();
			portcaps.Size = DMUS_PORTCAPSSize;

			int index = 0;
			while (directMusic.EnumPort(index++, ref portcaps) == S_OK && portcaps.GuidPort != Guid.Empty)
			{
				if (portcaps.Class == DMusClass.Output)
				{
					if ((soundDeviceType.HasFlag(SoundDeviceType.DirectX_EMU) && portcaps.Type == DMusType.WINMM_DRIVER) ||
						(soundDeviceType.HasFlag(SoundDeviceType.DirectX_ORG) && portcaps.Type != DMusType.WINMM_DRIVER))
					{
						Debug.WriteLine($"{index} {portcaps.GuidPort} {portcaps.Description}");
						devices.Add(new MidiOutDirectXDevice(portcaps.GuidPort, portcaps.Description, $"{portcaps.Class} - {portcaps.Type} - {portcaps.Flags} - {portcaps.EffectFlags}"));
					}
				}
				portcaps.GuidPort = Guid.Empty;
			}
		}
		/*

		// Port enumeration function

		public void PortEnumeration(int dwIndex, ref INFOPORT lpInfoPort)
		{
			HRESULT hr;
			DMUS_PORTCAPS portinf;

			// Set to 0 the DMUS_PORTCAPS structure  
			ZeroMemory(&portinf, sizeof(portinf));
			portinf.dwSize = sizeof(DMUS_PORTCAPS);

			//Call the DirectMusic8 member function to enumerate systems ports
			this.music8.EnumPort(dwIndex, &portinf);


			// Converts port description to char string 
			WideCharToMultiByte(CP_ACP, 0, portinf.wszDescription, -1,
								lpInfoPort->szPortDescription,
								sizeof(lpInfoPort->szPortDescription) /
								sizeof(lpInfoPort->szPortDescription[0]),0,0);


			// Copy the GUID of DMUS_PORTCAP structure to CMidiMusic port structure
			CopyMemory(&(lpInfoPort->guidSynthGUID), &portinf.guidPort, sizeof(GUID));

			lpInfoPort->dwClass = portinf.dwClass;
			lpInfoPort->dwEffectFlags = portinf.dwEffectFlags;
			lpInfoPort->dwFlags = portinf.dwFlags;
			lpInfoPort->dwMaxAudioChannels = portinf.dwMaxAudioChannels;
			lpInfoPort->dwMaxChannelGroups = portinf.dwMaxChannelGroups;
			lpInfoPort->dwMaxVoices = portinf.dwMaxVoices;
			lpInfoPort->dwType = portinf.dwType;

			return hr;
		}
		*/

	}
}
