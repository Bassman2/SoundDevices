using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct _DMUS_PORTCAPS
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
