using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaDevices.IO.Wave.Internal.WinMM
{
    internal class WaveInWinMMDevice
    {
        private static readonly int sizeWaveInCaps = Marshal.SizeOf(typeof(WaveInCaps));

        public static WaveInDeviceInfo[] GetDevices()
        {
            List<WaveInDeviceInfo> devices = new();

            
            //WaveInCaps waveInCaps = new();

            int num = waveInGetNumDevs();
            for (int i = 0; i < num; i++)
            {
                if (waveInGetDevCaps((IntPtr)i, out WaveInCaps waveInCaps, sizeWaveInCaps) == 0)
                {
                    devices.Add(new WaveInDeviceInfo
                    {
                        Name = waveInCaps.name,
                        Version = new Version(waveInCaps.driverVersion.Major, waveInCaps.driverVersion.Minor)
                    });
                }
            }

            return devices.ToArray();
        }


        //[DllImport("winmm.dll")]
        //private static extern int waveInGetDevCaps(IntPtr deviceID, ref MidiInCaps caps, int sizeOfMidiInCaps);

        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        public static extern int waveInGetDevCaps(IntPtr deviceID, out WaveInCaps waveinCaps, int sizeWaveInCaps);

        [DllImport("winmm.dll")]
        private static extern int waveInGetNumDevs();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WaveInCaps
        {
            /// <summary>
            /// wMid
            /// </summary>
            public short manufacturerId;
            /// <summary>
            /// wPid
            /// </summary>
            public short productId;
            /// <summary>
            /// vDriverVersion
            /// </summary>
            public MMVersion driverVersion;
            /// <summary>
            /// Product Name (szPname)
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string name;
            /// <summary>
            /// Supported formats (bit flags) dwFormats 
            /// </summary>
            public int supportedFormats;
            /// <summary>
            /// Supported channels (1 for mono 2 for stereo) (wChannels)
            /// Seems to be set to -1 on a lot of devices
            /// </summary>
            public short channels;
            /// <summary>
            /// wReserved1
            /// </summary>
            public short reserved;

            // extra WAVEINCAPS2 members
            public Guid manufacturerGuid;
            public Guid productGuid;
            public Guid nameGuid;
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
