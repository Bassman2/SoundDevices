using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct DMUS_CLOCKINFO7
    {
        uint dwSize;
        DMUS_CLOCKTYPE ctType;
        Guid guidClock;
    }
}
