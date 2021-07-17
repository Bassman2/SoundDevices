using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct DMUS_SYNTHSTATS
    {
        uint dwSize;
        uint dwValidStats;
        uint dwVoices;
        uint dwTotalCPU;
        uint dwCPUPerVoice;
        uint dwLostNotes;
        uint dwFreeMemory;
        int lPeakVolume;
    }
}
