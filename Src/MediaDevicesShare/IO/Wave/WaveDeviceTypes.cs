using System;
using System.Collections.Generic;
using System.Text;

namespace MediaDevices.IO.Wave
{
    [Flags]
    public enum WaveDeviceTypes
    {
        All = 0xffff,
        WinMM = 0x0001
    }
}
