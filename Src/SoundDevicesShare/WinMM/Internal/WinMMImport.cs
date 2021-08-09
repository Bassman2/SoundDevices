using System;
using System.Runtime.InteropServices;

namespace SoundDevices.WinMM.Internal
{
    internal static class WinMMImport
    {
        private const string WinMMLibrary = "winmm.dll";

        internal static readonly int MidiInCapsSize = Marshal.SizeOf(typeof(WinMMImport.MidiInCaps));
        internal static readonly int MidiOutCapsSize = Marshal.SizeOf(typeof(WinMMImport.MidiOutCaps));
        internal static readonly int MidiHeaderSize = Marshal.SizeOf(typeof(MidiHeader));

        internal static readonly int WaveInCapsSize = Marshal.SizeOf(typeof(WinMMImport.WaveInCaps));
        internal static readonly int WaveOutCapsSize = Marshal.SizeOf(typeof(WinMMImport.WaveOutCaps));

        /*
        CALLBACK_FUNCTION	The dwCallback parameter is a callback procedure address.
        CALLBACK_NULL	There is no callback mechanism. This value is the default setting.
        CALLBACK_THREAD	The dwCallback parameter is a thread identifier.
        CALLBACK_WINDOW	The dwCallback parameter is a window handle.
        MIDI_IO_STATUS
        */
        public const int CALLBACK_FUNCTION = 0x30000;

        public delegate void Callback(IntPtr handle, int msg, IntPtr instance, IntPtr param1, IntPtr param2);
        
        #region MIDI IN

        [DllImport(WinMMLibrary, EntryPoint = "midiInGetNumDevs")]
        public static extern int MidiInGetNumDevs();

        [DllImport(WinMMLibrary, EntryPoint = "midiInGetDevCaps", CharSet = CharSet.Auto)]
        public static extern int MidiInGetDevCaps(IntPtr deviceID, out MidiInCaps midiInCaps, int sizeMidiInCaps);

        [DllImport(WinMMLibrary, EntryPoint = "midiInOpen")]
        public static extern int MidiInOpen(out IntPtr handle, int deviceID, Callback callback, IntPtr instance, int flags);

        [DllImport(WinMMLibrary, EntryPoint = "midiInStart")]
        public static extern int MidiInStart(IntPtr handle);

        [DllImport(WinMMLibrary, EntryPoint = "midiInStop")]
        public static extern int MidiInStop(IntPtr handle);

        [DllImport(WinMMLibrary, EntryPoint = "midiInClose")]
        public static extern int MidiInClose(IntPtr handle);

        [DllImport(WinMMLibrary, EntryPoint = "midiInReset")]
        public static extern int MidiInReset(IntPtr handle);

        [DllImport(WinMMLibrary, EntryPoint = "midiInPrepareHeader")]
        public static extern int MidiInPrepareHeader(IntPtr handle, IntPtr headerPtr, int sizeOfMidiHeader);

        [DllImport(WinMMLibrary, EntryPoint = "midiInUnprepareHeader")]
        public static extern int MidiInUnprepareHeader(IntPtr handle, IntPtr headerPtr, int sizeOfMidiHeader);

        [DllImport(WinMMLibrary, EntryPoint = "midiInAddBuffer")]
        public static extern int MidiInAddBuffer(IntPtr handle, IntPtr headerPtr, int sizeOfMidiHeader);

        #endregion
        
        #region MIDI Out

        [DllImport(WinMMLibrary, EntryPoint = "midiOutGetNumDevs")]
        public static extern int MidiOutGetNumDevs();

        [DllImport(WinMMLibrary, EntryPoint = "midiOutGetDevCaps", CharSet = CharSet.Auto)]
        public static extern int MidiOutGetDevCaps(IntPtr deviceID, out MidiOutCaps midiOutCaps, int sizeMidiOutCaps);

        [DllImport(WinMMLibrary, EntryPoint = "midiOutOpen")]
        public static extern int MidiOutOpen(out IntPtr handle, int deviceID, Callback callback, IntPtr instance, int flags);

        [DllImport(WinMMLibrary, EntryPoint = "midiOutClose")]
        public static extern int MidiOutClose(IntPtr handle);
               
        [DllImport(WinMMLibrary, EntryPoint = "midiOutReset")]
        public static extern int MidiOutReset(IntPtr handle);

        [DllImport(WinMMLibrary, EntryPoint = "midiOutShortMsg")]
        public static extern int MidiOutShortMsg(IntPtr handle, int message);

        [DllImport(WinMMLibrary, EntryPoint = "midiOutPrepareHeader")]
        public static extern int MidiOutPrepareHeader(IntPtr handle, IntPtr headerPtr, int sizeOfMidiHeader);

        [DllImport(WinMMLibrary, EntryPoint = "midiOutUnprepareHeader")]
        public static extern int MidiOutUnprepareHeader(IntPtr handle, IntPtr headerPtr, int sizeOfMidiHeader);

        [DllImport(WinMMLibrary, EntryPoint = "midiOutLongMsg")]
        public static extern int MidiOutLongMsg(IntPtr handle, IntPtr headerPtr, int sizeOfMidiHeader);

        #endregion

        #region WAVE In

        [DllImport(WinMMLibrary, EntryPoint = "waveInGetNumDevs")]
        public static extern int WaveInGetNumDevs();

        [DllImport(WinMMLibrary, EntryPoint = "waveInGetDevCaps", CharSet = CharSet.Auto)]
        public static extern int WaveInGetDevCaps(IntPtr deviceID, out WaveInCaps waveinCaps, int sizeWaveInCaps);

        [DllImport(WinMMLibrary, EntryPoint = "waveInOpen")]
        public static extern int WaveInOpen(out IntPtr handle, int deviceID, [In, MarshalAs(UnmanagedType.LPStruct)] WaveFormatEx b, Callback callback, IntPtr instance, WaveOpenFlags dwFlags);

        [DllImport(WinMMLibrary, EntryPoint = "waveInStart")]
        public static extern int WaveInStart(IntPtr handle);

        [DllImport(WinMMLibrary, EntryPoint = "waveInStop")]
        public static extern int WaveInStop(IntPtr handle);

        [DllImport(WinMMLibrary, EntryPoint = "waveInClose")]
        public static extern int WaveInClose(IntPtr handle);

        [DllImport(WinMMLibrary, EntryPoint = "waveInReset")]
        public static extern int WaveInReset(IntPtr handle);

        #endregion

        #region WAVE Out

        [DllImport(WinMMLibrary, EntryPoint = "waveOutGetNumDevs")]
        public static extern int WaveOutGetNumDevs();

        [DllImport(WinMMLibrary, EntryPoint = "waveOutGetDevCaps", CharSet = CharSet.Auto)]
        public static extern int WaveOutGetDevCaps(IntPtr deviceID, out WaveOutCaps waveOutCaps, int sizeWaveOutCaps);

        [DllImport(WinMMLibrary, EntryPoint = "waveOutOpen")]
        public static extern int WaveOutOpen(out IntPtr handle, int deviceID, [In, MarshalAs(UnmanagedType.LPStruct)] WaveFormatEx b, Callback callback, IntPtr instance, WaveOpenFlags dwFlags);
                
        [DllImport(WinMMLibrary, EntryPoint = "waveOutReset")]
        public static extern int WaveOutReset(IntPtr handle);

        [DllImport(WinMMLibrary, EntryPoint = "waveOutClose")]
        public static extern int WaveOutClose(IntPtr handle);

        [DllImport(WinMMLibrary, EntryPoint = "waveOutPause")]
        public static extern int WaveOutPause(IntPtr handle);

        [DllImport(WinMMLibrary, EntryPoint = "waveOutRestart")]
        public static extern int WaveOutRestart(IntPtr handle);

        [DllImport(WinMMLibrary, EntryPoint = "waveOutSetVolume")]
        public static extern int WaveOutSetVolume(IntPtr handle, int volume);

        [DllImport(WinMMLibrary, EntryPoint = "waveOutGetVolume")]
        public static extern int WaveOutGetVolume(IntPtr handle, out int volume);

        [DllImport(WinMMLibrary, EntryPoint = "waveOutGetPitch")]
        public static extern int WaveOutGetPitch(IntPtr handle, out int pitch);

        [DllImport(WinMMLibrary, EntryPoint = "waveOutSetPitch")]
        public static extern int WaveOutSetPitch(IntPtr handle, int pitch);

        [DllImport(WinMMLibrary, EntryPoint = "waveOutGetPlaybackRate")]
        public static extern int WaveOutGetPlaybackRate(IntPtr handle, out int rate);

        [DllImport(WinMMLibrary, EntryPoint = "waveOutSetPlaybackRate")]
        public static extern int WaveOutSetPlaybackRate(IntPtr handle, int rate);

        [DllImport(WinMMLibrary, EntryPoint = "waveOutGetPosition")]
        public static extern int WaveOutGetPosition(IntPtr handle, [In, Out, MarshalAs(UnmanagedType.LPStruct)] MMTIME lpInfo, int size);

        [DllImport(WinMMLibrary, EntryPoint = "waveOutBreakLoop")]
        public static extern int WaveOutBreakLoop(IntPtr handle);

        #endregion

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

        [StructLayout(LayoutKind.Sequential)]
        internal struct MidiHeader
        {
            #region MidiHeader Members

            /// <summary>
            /// Pointer to MIDI data.
            /// </summary>
            public IntPtr data;

            /// <summary>
            /// Size of the buffer.
            /// </summary>
            public int bufferLength;

            /// <summary>
            /// Actual amount of data in the buffer. This value should be less than 
            /// or equal to the value given in the dwBufferLength member.
            /// </summary>
            public int bytesRecorded;

            /// <summary>
            /// Custom user data.
            /// </summary>
            public int user;

            /// <summary>
            /// Flags giving information about the buffer.
            /// </summary>
            public int flags;

            /// <summary>
            /// Reserved; do not use.
            /// </summary>
            public IntPtr next;

            /// <summary>
            /// Reserved; do not use.
            /// </summary>
            public int reserved;

            /// <summary>
            /// Offset into the buffer when a callback is performed. (This 
            /// callback is generated because the MEVT_F_CALLBACK flag is 
            /// set in the dwEvent member of the MidiEventArgs structure.) 
            /// This offset enables an application to determine which 
            /// event caused the callback. 
            /// </summary>
            public int offset;

            /// <summary>
            /// Reserved; do not use.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public int[] reservedArray;

            #endregion
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

        /// <summary>
        /// From WAVEFORMAT
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public class WaveFormat
        {
            public short wFormatTag;        /* format type */
            public short nChannels;         /* number of channels (i.e. mono, stereo, etc.) */
            public int nSamplesPerSec;    /* sample rate */
            public int nAvgBytesPerSec;   /* for buffer estimation */
            public short nBlockAlign;       /* block size of data */
        }

        /// <summary>
        /// From WAVEFORMATEX
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class WaveFormatEx : WaveFormat
        {
            public short wBitsPerSample;
            public short cbSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class MMTIME
        {
            public MMTimeFlags wType;
            public int u;
            public int x;
        }

        [Flags]
        public enum MMTimeFlags
        {
            MS = 0x0001,        /* time in milliseconds */
            Samples = 0x0002,   /* number of wave samples */
            Bytes = 0x0004,     /* current byte offset */
            SMPTE = 0x0008,     /* SMPTE time */
            Midi = 0x0010,      /* MIDI time */
            Ticks = 0x0020      /* Ticks within MIDI stream */
        }

        [Flags]
        public enum WaveOpenFlags
        {
            None = 0,
            FormatQuery = 0x0001,
            AllowSync = 0x0002,
            Mapped = 0x0004,
            FormatDirect = 0x0008,
            Null = 0x00000000,      /* no callback */
            Window = 0x00010000,    /* dwCallback is a HWND */
            Thread = 0x00020000,    /* dwCallback is a THREAD */
            Function = 0x00030000,  /* dwCallback is a FARPROC */
            Event = 0x00050000      /* dwCallback is an EVENT Handle */
        }

    }
}
