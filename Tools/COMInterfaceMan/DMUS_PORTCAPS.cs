using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


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
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        string wszDescription;
    }
}
