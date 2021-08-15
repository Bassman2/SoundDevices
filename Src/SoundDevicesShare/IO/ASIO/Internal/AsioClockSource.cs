using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.ASIO.Internal
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Ansi)]
    internal struct AsioClockSource
    {
        public int index;
        public int associatedChannel;
        public int associatedGroup;
        public int isCurrentSource;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string name;
    }
}
