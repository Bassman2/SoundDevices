using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.WinMM.Internal
{
    /// <summary>
    /// From WAVEFORMATEX
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class WaveFormatEx : WaveFormat
    {
        public short wBitsPerSample;
        public short cbSize;
    }
}
