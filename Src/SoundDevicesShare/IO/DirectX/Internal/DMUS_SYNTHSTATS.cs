using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.DirectX.Internal
{
    internal struct DMUS_SYNTHSTATS
    {
        int dwSize;             /* Size in bytes of the structure */
        int dwValidStats;       /* Flags indicating which fields below are valid. */
        int dwVoices;           /* Average number of voices playing. */
        int dwTotalCPU;         /* Total CPU usage as percent * 100. */
        int dwCPUPerVoice;      /* CPU per voice as percent * 100. */
        int dwLostNotes;        /* Number of notes lost in 1 second. */
        int dwFreeMemory;       /* Free memory in bytes */
        int lPeakVolume;        /* Decibel level * 100. */
    }
}
