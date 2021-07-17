using MediaDevices.IO.MIDI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaDevices.IO.Internal.DirectMusic
{
    public class MidiInDirectMusicDevice : MidiInDevice
    {
        public static void AddInternalDevices(List<MidiInDevice> devices)
        {
            DirectMusicDevice.GetDevices();
        }
    }
}
