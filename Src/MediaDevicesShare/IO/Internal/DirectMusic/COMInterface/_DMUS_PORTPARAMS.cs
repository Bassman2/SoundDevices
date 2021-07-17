using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct _DMUS_PORTPARAMS
    {
        uint dwSize;
        uint dwValidParams;
        uint dwVoices;
        uint dwChannelGroups;
        uint dwAudioChannels;
        uint dwSampleRate;
        uint dwEffectFlags;
        [MarshalAs(UnmanagedType.Bool)]
        bool fShare;
    }
}
