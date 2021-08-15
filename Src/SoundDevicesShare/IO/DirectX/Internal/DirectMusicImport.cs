using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.DirectX.Internal
{
    internal class DirectMusicImport
    {
		//                                                                    dddddddd-dddd-dddd-dddd-dddddddddddd
		private static readonly Guid CLSID_DirectMusicPerformance = new Guid("d2ac2881-b39b-11d1-8704-00600893b1bd");
		private static readonly Guid CLSID_DirectMusicLoader      = new Guid("d2ac2892-b39b-11d1-8704-00600893b1bd");

		//IDirectMusic music;             //Main DirectMusic COM interfaces
		//IDirectMusic8 music8;
		IDirectMusicLoader8 directMusicLoader8;
		//IDirectMusicPerformance8 performance;
		//IDirectMusicSegment8 pSegment;
		//IDirectMusicPort8 pMusicPort;
		//IDirectMusicSegmentState pSegmentState;
		//IDirectMusicSegmentState8 pSegmentState8;
		//IDirectMusicAudioPath8 p3DAudioPath;
		//IDirectSound3DBuffer8 pDSB;

		public void Initialize()
		{
			//comType = Type.GetTypeFromCLSID(guid);

			//bool isCOM = comType.IsCOMObject;

			var typeDirectMusicLoader = Type.GetTypeFromCLSID(CLSID_DirectMusicLoader);
			object obj = Activator.CreateInstance(typeDirectMusicLoader);
			directMusicLoader8 = (IDirectMusicLoader8)obj;

			////if (FAILED(hr = CoCreateInstance(CLSID_DirectMusicLoader, NULL, CLSCTX_INPROC, IID_IDirectMusicLoader8, (void**)&m_pLoader))) return hr;
			//loader = new DirectMusicLoader();

			////if (FAILED(hr = CoCreateInstance(CLSID_DirectMusicPerformance, NULL, CLSCTX_INPROC, IID_IDirectMusicPerformance8, (void**)&m_pPerformance))) return hr;
			//this.performance = DirectMusicPerformance();
						
			//performance.Init(out this.music, IntPtr.Zero, IntPtr.Zero);
			//this.music8 = this.music;
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
