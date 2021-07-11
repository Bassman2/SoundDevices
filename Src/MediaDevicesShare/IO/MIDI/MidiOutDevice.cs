using MediaDevices.IO.Internal.WinMM;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaDevices.IO.MIDI
{
    public class MidiOutDevice : SoundDevice
    {
        public static MidiOutDevice[] GetDevices(MidiDeviceTypes midiDeviceTypes)
        {
            List<MidiOutDevice> devices = new();
            if (OperatingSystem.IsWindows() && midiDeviceTypes.HasFlag(MidiDeviceTypes.WinMM))
            {
                MidiOutWinMMDevice.AddInternalDevices(devices);
            }
            if (OperatingSystem.IsAndroid())
            {

            }
            if (OperatingSystem.IsLinux())
            {

            }
            if (OperatingSystem.IsIOS())
            {

            }
            if (OperatingSystem.IsMacOS())
            {

            }
            return devices.ToArray();
        }
    }
}
