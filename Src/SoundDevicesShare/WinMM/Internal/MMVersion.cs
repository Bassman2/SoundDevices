using System;
using System.Runtime.InteropServices;

namespace SoundDevices.WinMM.Internal
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
