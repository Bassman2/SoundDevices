using System;
using System.Collections.Generic;
using System.Text;

namespace MediaDevices.IO
{
    public enum SoundInterfaceType
    {
        // Windows
        WinMM,
        ASIO,
        DirectSound,
        
        // Linux

        /// <summary>
        /// JACK Audio Connection Kit
        /// </summary>
        JACK
    }
}
