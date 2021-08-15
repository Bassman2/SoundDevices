using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.DirectX.Internal
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct DMUS_BUFFERDESC
    {
        int dwSize;
        int dwFlags;
        Guid guidBufferFormat;
        int cbBuffer;
    }
}
