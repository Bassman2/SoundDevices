using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.ASIO.Internal
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct AsioBufferInfo
    {
        public int isInput;
        public int channelNum;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public IntPtr[] buffers;
    };
}
