using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaDevices.IO.MIDI.Internal.WinMM
{
    public class MidiOutWinMMDevice
    {
        private static readonly int sizeMidiOutCaps = Marshal.SizeOf(typeof(MidiOutCaps));

        public static MidiOutDeviceInfo[] GetDevices()
        {
            List<MidiOutDeviceInfo> devices = new();

            for (int i = 0; i < midiOutGetNumDevs(); i++)
            {
                if (midiOutGetDevCaps((IntPtr)i, out MidiOutCaps midiOutCaps, sizeMidiOutCaps) == 0)
                {
                    devices.Add(new MidiOutDeviceInfo
                    {
                        Name = midiOutCaps.name,
                        Version = new Version(midiOutCaps.driverVersion.Major, midiOutCaps.driverVersion.Minor)
                    });
                }
            }

            return devices.ToArray();
        }

        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        private static extern int midiOutGetDevCaps(IntPtr deviceID, out MidiOutCaps midiOutCaps, int sizeMidiOutCaps);

        [DllImport("winmm.dll")]
        private static extern int midiOutGetNumDevs();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct MidiOutCaps
        {
            /// <summary>
            /// Manufacturer identifier of the device driver for the Midi output device. 
            /// </summary>
            public short mid;

            /// <summary>
            /// Product identifier of the Midi output device. 
            /// </summary>
            public short pid;

            /// <summary>
            /// Version number of the device driver for the Midi output device. 
            /// The high-order byte is the major version number, and the low-order byte is the minor version number. 
            /// </summary>
            public MMVersion driverVersion;

            /// <summary>
            /// Product name in a null-terminated string.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string name;

            /// <summary>
            /// Flags describing the type of the Midi output device. 
            /// </summary>
            public short technology;

            /// <summary>
            /// Number of voices supported by an internal synthesizer device. 
            /// If the device is a port, this member is not meaningful and is set to 0. 
            /// </summary>
            public short voices;

            /// <summary>
            /// Maximum number of simultaneous notes that can be played by an internal synthesizer device. 
            /// If the device is a port, this member is not meaningful and is set to 0. 
            /// </summary>
            public short notes;

            /// <summary>
            /// Channels that an internal synthesizer device responds to, where the least significant bit refers to channel 0 and the most significant bit to channel 15. 
            /// Port devices that transmit on all channels set this member to 0xFFFF. 
            /// </summary>
            public short channelMask;

            /// <summary>
            /// Optional functionality supported by the device. 
            /// </summary>
            public int support;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct MMVersion
        {
            public byte Minor;
            public byte Major;
            public short Dummy;
        }
    }
}
