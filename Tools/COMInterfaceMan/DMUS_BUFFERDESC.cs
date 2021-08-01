using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct DMUS_BUFFERDESC
    {
        uint dwSize;
        uint dwFlags;
        Guid guidBufferFormat;
        uint cbBuffer;
    }
}
