using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.WinMM
{
    internal static class WinMMImport
    {
        [DllImport("winmm.dll", EntryPoint = "midiInGetNumDevs")]
        public static extern int MidiInGetNumDevs();

        [DllImport("winmm.dll", EntryPoint = "midiInGetDevCaps", CharSet = CharSet.Auto)]
        public static extern int MidiInGetDevCaps(IntPtr deviceID, out MidiInCaps midiInCaps, int sizeMidiInCaps);


        [DllImport("winmm.dll", EntryPoint = "midiOutGetNumDevs")]
        public static extern int MidiOutGetNumDevs();

        [DllImport("winmm.dll", EntryPoint = "midiOutGetDevCaps", CharSet = CharSet.Auto)]
        public static extern int MidiOutGetDevCaps(IntPtr deviceID, out MidiOutCaps midiOutCaps, int sizeMidiOutCaps);


        [DllImport("winmm.dll", EntryPoint = "waveInGetNumDevs")]
        public static extern int WaveInGetNumDevs();

        [DllImport("winmm.dll", EntryPoint = "waveInGetDevCaps", CharSet = CharSet.Auto)]
        public static extern int WaveInGetDevCaps(IntPtr deviceID, out WaveInCaps waveinCaps, int sizeWaveInCaps);


        [DllImport("winmm.dll", EntryPoint = "waveOutGetNumDevs")]
        public static extern int WaveOutGetNumDevs();

        [DllImport("winmm.dll", EntryPoint = "waveOutGetDevCaps", CharSet = CharSet.Auto)]
        public static extern int WaveOutGetDevCaps(IntPtr deviceID, out WaveOutCaps waveOutCaps, int sizeWaveOutCaps);

        
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

    }
}
