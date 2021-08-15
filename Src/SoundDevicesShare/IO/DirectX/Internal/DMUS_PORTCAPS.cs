using System;
using System.Runtime.InteropServices;

namespace SoundDevices.IO.DirectX.Internal
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct DMUS_PORTCAPS
    {
        public int Size;
        public DMusFlag Flags;
        public Guid GuidPort;
        public DMusClass Class;
        public DMusType Type;
        public int MemorySize;
        public int MaxChannelGroups;
        public int MaxVoices;
        public int MaxAudioChannels;
        public DMusEffect EffectFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string Description;
    }

    internal enum DMusClass : int
    {
        Input = 0,
        Output = 1
    }

    internal enum DMusType : int
    {
        WINMM_DRIVER = 0,
        USER_MODE_SYNTH = 1,
        KERNEL_MODE = 2
    }

    [Flags]
    internal enum DMusEffect : int
    {
        NONE = 0x00000000,
        REVERB = 0x00000001,
        CHORUS = 0x00000002,
        DELAY = 0x00000004
    }

    [Flags]
    internal enum DMusFlag : int
    {
        DLS = 0x00000001,   /* Supports DLS downloading and DLS level 1. */
        EXTERNAL = 0x00000002,   /* External MIDI module. */
        SOFTWARESYNTH = 0x00000004,   /* Software synthesizer. */
        MEMORYSIZEFIXED = 0x00000008,   /* Memory size is fixed. */
        GMINHARDWARE = 0x00000010,   /* GM sound set is built in, no need to download. */
        GSINHARDWARE = 0x00000020,   /* GS sound set is built in. */
        XGINHARDWARE = 0x00000040,   /* XG sound set is built in. */
        DIRECTSOUND = 0x00000080,   /* Connects to DirectSound via a DSound buffer. */
        SHAREABLE = 0x00000100,   /* Synth can be actively shared by multiple apps at once. */
        DLS2 = 0x00000200,   /* Supports DLS2 instruments. */
        AUDIOPATH = 0x00000400,   /* Multiple outputs can be connected to DirectSound for audiopaths. */
        WAVE = 0x00000800,   /* Supports streaming and one shot waves. */
        SYSTEMMEMORY = 0x7FFFFFFF   /* Sample memory is system memory. */
    }
}
