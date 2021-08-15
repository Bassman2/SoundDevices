using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.WinMM.Internal
{
    /// <summary>
    /// From WAVEFORMAT
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class WaveFormat
    {
        public short wFormatTag;        /* format type */
        public short nChannels;         /* number of channels (i.e. mono, stereo, etc.) */
        public int nSamplesPerSec;    /* sample rate */
        public int nAvgBytesPerSec;   /* for buffer estimation */
        public short nBlockAlign;       /* block size of data */
    }
}
