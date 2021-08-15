using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO
{
    [Flags]
    public enum SoundDeviceType
    {
        All = 0xffffff,
        // Windows
        WinMM = 0x00000001,
        DirectX_EMU = 0x00000010,
        DirectX_ORG = 0x00000020,
        DirectX = 0x00000030,
        ASIO = 0x00000100,
        // Linux
        ALSA = 0x00001000,
        // Mac
        CoreAudio = 0x00010000
    }
}
