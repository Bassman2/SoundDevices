using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.WinMM
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MMVersion
    {
        public byte Minor;
        public byte Major;
        public short Dummy;

        public static implicit operator Version(MMVersion v) => new(v.Major, v.Minor);
    }
}
