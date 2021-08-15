using SoundDevices.IO.ASIO;
using SoundDevices.IO.DirectX;
using SoundDevices.IO.WinMM;
using SoundDevices.IO.CoreMIDI;
using SoundDevices.IO.ALSA;
using System;
using System.Collections.Generic;

namespace SoundDevices.IO
{
    public abstract class WaveInDevice : SoundDevice
    {
        public static IEnumerable<WaveInDevice> GetDevices(SoundDeviceType soundDeviceType = SoundDeviceType.All)
        {
            List<WaveInDevice> devices = new();
            if (OperatingSystem.IsWindows())
            {
                if (soundDeviceType.HasFlag(SoundDeviceType.WinMM))
                {
                    WaveInWinMMDevice.AddDevices(soundDeviceType, devices);
                }
                if ((soundDeviceType & SoundDeviceType.DirectX) != 0)
                {
                    WaveInDirectXDevice.AddDevices(soundDeviceType, devices);
                }
                //if (soundDeviceTypes.HasFlag(SoundDeviceType.ASIO))
                //{
                //    WaveInASIODevice.AddDevices(devices);
                //}
            }
            if (OperatingSystem.IsLinux())
            {
                if (soundDeviceType.HasFlag(SoundDeviceType.ALSA))
                {
                    WaveInALSADevice.AddDevices(soundDeviceType, devices);
                }
            }
            if (OperatingSystem.IsMacOS())
            {
                if (soundDeviceType.HasFlag(SoundDeviceType.CoreAudio))
                {
                    WaveInCoreAudioDevice.AddDevices(soundDeviceType, devices);
                }
            }
            return devices;
        }

        public abstract void Open(WaveFormat waveFormat = null);
        public abstract void Start();
        public abstract void Stop();
        public abstract void Reset();
        public abstract void Close();
    }
}
