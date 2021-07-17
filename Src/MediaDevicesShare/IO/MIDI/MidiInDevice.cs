using MediaDevices.IO.Internal.DirectMusic;
using MediaDevices.IO.Internal.WinMM;
using System;
using System.Collections.Generic;

namespace MediaDevices.IO.MIDI
{
    public class MidiInDevice : SoundDevice
    {
        public static MidiInDevice[] GetDevices(MidiDeviceTypes midiDeviceTypes)
        {
            List<MidiInDevice> devices = new();
            if (OperatingSystem.IsWindows())
            {
                if (midiDeviceTypes.HasFlag(MidiDeviceTypes.WinMM))
                {
                    MidiInWinMMDevice.AddInternalDevices(devices);
                }
                if (midiDeviceTypes.HasFlag(MidiDeviceTypes.DirectMusic))
                {
                    MidiInDirectMusicDevice.AddInternalDevices(devices);
                }
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
