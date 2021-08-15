using SoundDevices.IO.DirectX.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.DirectX
{
    public class MidiInDirectXDevice : MidiInDevice
    {
        internal static void AddDevices(List<MidiInDevice> devices)
        {
            new DirectMusicImport().Initialize();
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
