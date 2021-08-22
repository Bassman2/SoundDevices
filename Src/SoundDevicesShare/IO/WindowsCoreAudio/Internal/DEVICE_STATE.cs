﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.WindowsCoreAudio.Internal
{
    [Flags]
    public enum DEVICE_STATE : uint
    {
        DEVICE_STATE_ACTIVE = 0x00000001,
        DEVICE_STATE_DISABLED = 0x00000002,
        DEVICE_STATE_NOTPRESENT = 0x00000004,
        DEVICE_STATE_UNPLUGGED = 0x00000008,
        DEVICE_STATEMASK_ALL = 0x0000000F
    }
}
