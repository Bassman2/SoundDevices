using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices
{
    public class WaveFormat
    {
        public WaveFormat()
        {
            this.Channels = 2;
            this.SampleRate = 44100;
            this.BitRate = 16;
        }

        /// <summary>
        /// 1 = Mono, 2 = Stereo
        /// </summary>
        public int Channels { get; set; }

        /// <summary>
        /// Sampels per second
        /// </summary>
        public int SampleRate { get; set; }

        /// <summary>
        /// Bits per sample
        /// </summary>
        public int BitRate { get; set; }
    }
}
