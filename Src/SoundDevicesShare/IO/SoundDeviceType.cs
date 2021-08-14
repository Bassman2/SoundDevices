using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO
{
    [Flags]
    public enum SoundDeviceType
    {
        All = 0xffff,
        // Windows
        WinMM = 0x0001,
        DirectX = 0x0002,
        ASIO = 0x0004,
        // Linux
        ALSA = 0x0010,
        // Mac
        CoreAudio = 0x0100
    }
}
