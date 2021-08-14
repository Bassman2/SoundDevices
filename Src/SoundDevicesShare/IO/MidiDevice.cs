using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO
{
    public abstract class MidiDevice : SoundDevice
    {
        public static IEnumerable<MidiInDevice> GetDevices(SoundDeviceType soundDeviceTypes = SoundDeviceType.All)
        {
            List<MidiInDevice> devices = new();
            if (OperatingSystem.IsWindows())
            {
                if (soundDeviceTypes.HasFlag(SoundDeviceType.WinMM))
                {
                    //MidiInWinMMDevice.AddDevices(devices);
                }
                if (soundDeviceTypes.HasFlag(SoundDeviceType.DirectX))
                {
                    //MidiInDirectXDevice.AddDevices(devices);
                }
            }
            if (OperatingSystem.IsLinux())
            {
                if (soundDeviceTypes.HasFlag(SoundDeviceType.ALSA))
                {
                    //MidiInALSADevice.AddDevices(devices);
                }
            }
            if (OperatingSystem.IsMacOS())
            {
                if (soundDeviceTypes.HasFlag(SoundDeviceType.CoreAudio))
                {
                    //MidiInCoreAudioDevice.AddDevices(devices);
                }
            }
            return devices;
        }
    }
}
