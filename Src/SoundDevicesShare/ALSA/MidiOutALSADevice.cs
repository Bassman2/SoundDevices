﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.ALSA
{
    public class MidiOutALSADevice : MidiOutDevice
    {
        internal static void AddDevices(List<MidiOutDevice> devices)
        {
            
        }

        public override void Open()
        { }

        public override void Close()
        { }

        public override void Reset()
        { }

        public override void Send(int msg)
        { }

        public override void Send(byte[] data)
        { }
    }
}
