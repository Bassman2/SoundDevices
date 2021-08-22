using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.WindowsCoreAudio.Internal
{
    internal enum EStgmAccess
    {
        STGM_READ = 0x00000000,
        STGM_WRITE = 0x00000001,
        STGM_READWRITE = 0x00000002
    }
}
