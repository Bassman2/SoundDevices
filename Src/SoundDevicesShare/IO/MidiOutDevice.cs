using SoundDevices.IO.ALSA;
using SoundDevices.IO.CoreAudio;
using SoundDevices.IO.DirectX;
using SoundDevices.IO.WinMM;
using System;
using System.Collections.Generic;

namespace SoundDevices.IO
{
    public abstract class MidiOutDevice : SoundDevice
    {
        public static IEnumerable<MidiOutDevice> GetDevices(SoundDeviceType soundDeviceType = SoundDeviceType.All)
        {
            List<MidiOutDevice> devices = new();
            if (OperatingSystem.IsWindows())
            {
                if (soundDeviceType.HasFlag(SoundDeviceType.WinMM))
                {
                    MidiOutWinMMDevice.AddDevices(soundDeviceType, devices);
                }
                if ((soundDeviceType & SoundDeviceType.DirectX) != 0)
                {
                    MidiOutDirectXDevice.AddDevices(soundDeviceType, devices);
                }
            }
            if (OperatingSystem.IsLinux())
            {
                if (soundDeviceType.HasFlag(SoundDeviceType.ALSA))
                {
                    MidiOutALSADevice.AddDevices(soundDeviceType, devices);
                }
            }
            if (OperatingSystem.IsMacOS())
            {
                if (soundDeviceType.HasFlag(SoundDeviceType.CoreAudio))
                {
                    MidiOutCoreMidiDevice.AddDevices(soundDeviceType, devices);
                }
            }
            return devices.ToArray();
        }

        public abstract void Open();
        public abstract void Send(int msg);
        public abstract void Send(byte[] data);
        public abstract void Reset();
        public abstract void Close();

    }
}
