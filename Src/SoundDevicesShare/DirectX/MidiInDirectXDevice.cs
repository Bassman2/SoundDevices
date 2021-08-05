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
    }
}
