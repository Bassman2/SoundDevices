using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct DMUS_PORTCAPS
    {
        uint dwSize;
        uint dwFlags;
        Guid guidPort;
        uint dwClass;
        uint dwType;
        uint dwMemorySize;
        uint dwMaxChannelGroups;
        uint dwMaxVoices;
        uint dwMaxAudioChannels;
        uint dwEffectFlags;
    }
}
