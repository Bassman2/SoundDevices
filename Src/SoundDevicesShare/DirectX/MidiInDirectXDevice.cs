using SoundDevices.DirectX.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.DirectX
{
    public class MidiInDirectXDevice : MidiInDevice
    {
        internal static void AddDevices(List<MidiInDevice> devices)
        {
            DirectMusicDevice.GetDevices();
        }

        public override void Open()
        { }

        public override void Close()
        { }

        public override void Reset()
        { }

        public override void Start()
        { }

        public override void Stop()
        { }
    }
}
