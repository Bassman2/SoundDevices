using System;
using System.Collections.Generic;
using System.Text;

namespace MediaDevices.IO.Wave
{
    [Flags]
    public enum WaveDeviceTypes
    {
        All = 0xffff,
        WinMM = 0x0001,
        ASIO = 0x0002,
        DirectSound = 0x0004
    }
}
