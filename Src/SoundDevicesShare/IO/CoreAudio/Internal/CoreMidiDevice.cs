using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.CoreAudio.Internal
{
	// https://developer.apple.com/documentation/coremidi

	// https://developer.apple.com/documentation/coremidi/1495164-midigetnumberofdevices

	// https://apisof.net/catalog/e61ee9a2-d72e-f028-238a-5f135f9bc063

	// https://github.com/kjpou1/maccore

	// https://apisof.net/catalog/e61ee9a2-d72e-f028-238a-5f135f9bc063

	public class CoreMidiDevice
    {
        public const string CoreMidiLibrary = "/System/Library/Frameworks/CoreMIDI.framework/CoreMIDI";

        [DllImport(CoreMidiLibrary)]
        private extern static void MIDIRestart();

        [DllImport(CoreMidiLibrary)]
        private extern static int MIDIGetNumberOfExternalDevices();


		[DllImport(CoreMidiLibrary)]
		private extern static int MIDIGetNumberOfDestinations();
		[DllImport(CoreMidiLibrary)]
		private extern static int MIDIGetNumberOfSources();
				
		[DllImport(CoreMidiLibrary)]
		private extern static int MIDIGetNumberOfDevices();

		public static int ExternalDeviceCount
		{
			get
			{
				return MIDIGetNumberOfExternalDevices();
			}
		}

		public static int DeviceCount
		{
			get
			{
				return MIDIGetNumberOfDevices();
			}
		}

		[DllImport(CoreMidiLibrary)]
		private extern static IntPtr MIDIGetExternalDevice(int item);

		[DllImport(CoreMidiLibrary)]
		private extern static IntPtr MIDIGetDevice(int item);
	}
}
