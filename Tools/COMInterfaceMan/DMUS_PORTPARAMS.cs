using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct DMUS_PORTPARAMS
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
