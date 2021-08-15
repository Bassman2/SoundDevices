using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.CoreMIDI
{
    public class MidiOutCoreAudioDevice : MidiOutDevice
    {
        internal static void AddDevices(SoundDeviceType soundDeviceType, List<MidiOutDevice> devices)
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
