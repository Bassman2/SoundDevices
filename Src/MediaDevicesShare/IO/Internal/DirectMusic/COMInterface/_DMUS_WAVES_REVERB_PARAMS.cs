using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct _DMUS_WAVES_REVERB_PARAMS
    {
        float fInGain;
        float fReverbMix;
        float fReverbTime;
        float fHighFreqRTRatio;
    }
}
