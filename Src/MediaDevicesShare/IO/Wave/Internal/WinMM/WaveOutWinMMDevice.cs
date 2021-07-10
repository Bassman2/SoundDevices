using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaDevices.IO.Wave.Internal.WinMM
{
    internal class WaveOutWinMMDevice
    {
        private static readonly int sizeWaveOutCaps = Marshal.SizeOf(typeof(WaveOutCaps));

        public static WaveOutDeviceInfo[] GetDevices()
        {
            List<WaveOutDeviceInfo> devices = new();

            for (int i = 0; i < waveOutGetNumDevs(); i++)
            {
                if (waveOutGetDevCaps((IntPtr)i, out WaveOutCaps waveOutCaps, sizeWaveOutCaps) == 0)
                {
                    devices.Add(new WaveOutDeviceInfo
                    {
                        Name = waveOutCaps.name,
                        Version = new Version(waveOutCaps.driverVersion.Major, waveOutCaps.driverVersion.Minor)
                    });
                }
            }

            return devices.ToArray();
        }

        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        public static extern int waveOutGetDevCaps(IntPtr deviceID, out WaveOutCaps waveOutCaps, int sizeWaveOutCaps);

        [DllImport("winmm.dll")]
        private static extern int waveOutGetNumDevs();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WaveOutCaps
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
            /// <summary>
            /// Optional functionality supported by the device
            /// </summary>
            public int support; // = new WaveOutSupport();

            // extra WAVEOUTCAPS2 members
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
