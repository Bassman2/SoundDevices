using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct _DMUS_CLOCKINFO8
    {
        uint dwSize;
        DMUS_CLOCKTYPE ctType;
        Guid guidClock;
        uint dwFlags;
    }
}
