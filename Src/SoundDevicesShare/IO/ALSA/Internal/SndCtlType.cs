using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.ALSA.Internal
{
    
    public enum SndCtlType
    {
        /** Kernel level CTL */
        SND_CTL_TYPE_HW,
        /** Shared memory client CTL */
        SND_CTL_TYPE_SHM,
        /** INET client CTL (not yet implemented) */
        SND_CTL_TYPE_INET,
        /** External control plugin */
        SND_CTL_TYPE_EXT
    }
}
