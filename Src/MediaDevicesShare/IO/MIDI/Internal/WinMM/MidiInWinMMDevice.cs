using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaDevices.IO.MIDI.Internal.WinMM
{
    internal class MidiInWinMMDevice
    {
        private static readonly int sizeMidiInCaps = Marshal.SizeOf(typeof(MidiInCaps));

        public static MidiInDeviceInfo[] GetDevices()
        {
            List<MidiInDeviceInfo> devices = new();

            for (int i = 0; i < midiInGetNumDevs(); i++)
            {
                if (midiInGetDevCaps((IntPtr)i, out MidiInCaps midiInCaps, sizeMidiInCaps) == 0)
                {
                    devices.Add(new MidiInDeviceInfo 
                    { 
                        Name = midiInCaps.name,
                        Version = new Version(midiInCaps.driverVersion.Major, midiInCaps.driverVersion.Minor)
                    });
                }
            }

            return devices.ToArray();
        }


        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        private static extern int midiInGetDevCaps(IntPtr deviceID, out MidiInCaps midiInCaps, int sizeMidiInCaps);

        [DllImport("winmm.dll")]
        private static extern int midiInGetNumDevs();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct MidiInCaps
        {
            /// <summary>
            /// Manufacturer identifier of the device driver for the MIDI input device.  
            /// </summary>
            public short mid;

            /// <summary>
            /// Product identifier of the MIDI input device.
            /// </summary>
            public short pid;

            /// <summary>
            /// Version number of the device driver for the MIDI input device. 
            /// The high-order byte is the major version number, and the low-order byte is the minor version number.
            /// </summary>
            public MMVersion driverVersion;

            /// <summary>
            /// Product name in a null-terminated string.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string name;

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
