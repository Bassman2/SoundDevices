﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.ALSA
{
    internal sealed class WaveInALSADevice : WaveInDevice
    {
        internal static void AddDevices(SoundDeviceType soundDeviceType, List<WaveInDevice> devices)
        {
            
        }

        public override void Dispose()
        { }

        public override void Open(WaveFormat waveFormat = null)
        { }

        public override void Start()
        { }

        public override void Stop()
        { }

        public override void Reset()
        { }

        public override void Close()
        { }
    }
}
