using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SoundDevices.IO.ALSA
{
    internal sealed class WaveOutALSADevice : WaveOutDevice
    {
        internal static void AddDevices(SoundDeviceType soundDeviceType, List<WaveOutDevice> devices) 
        {
            
        }

        public override void Dispose()
        { }

        public override void Open(WaveFormat waveFormat = null)
        { }

        public override void Play(Stream stream)
        {

        }

        public override void Play(byte[] buffer, int offset, int count)
        {

        }

        public override void Reset()
        { }

        public override void Close()
        { }
    }
}
