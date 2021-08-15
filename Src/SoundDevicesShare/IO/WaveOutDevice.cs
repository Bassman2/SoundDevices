using SoundDevices.IO.DirectX;
using SoundDevices.IO.WinMM;
using SoundDevices.IO.ASIO;
using SoundDevices.IO.CoreMIDI;
using SoundDevices.IO.ALSA;
using System;
using System.Collections.Generic;
using System.IO;

namespace SoundDevices.IO
{
    public abstract class WaveOutDevice : SoundDevice
    {
        public static IEnumerable<WaveOutDevice> GetDevices(SoundDeviceType soundDeviceTypes = SoundDeviceType.All)
        {
            List<WaveOutDevice> devices = new();
            if (OperatingSystem.IsWindows())
            {
                if (soundDeviceTypes.HasFlag(SoundDeviceType.WinMM))
                {
                    WaveOutWinMMDevice.AddDevices(devices);
                }
                if (soundDeviceTypes.HasFlag(SoundDeviceType.DirectX))
                {
                    WaveOutDirectXDevice.AddDevices(devices);
                }
                //if (soundDeviceTypes.HasFlag(SoundDeviceType.ASIO))
                //{
                //    WaveOutASIODevice.AddDevices(devices);
                //}
            }
            if (OperatingSystem.IsLinux())
            {
                if (soundDeviceTypes.HasFlag(SoundDeviceType.ALSA))
                {
                    WaveOutALSADevice.AddDevices(devices);
                }
            }
            if (OperatingSystem.IsMacOS())
            {
                if (soundDeviceTypes.HasFlag(SoundDeviceType.CoreAudio))
                {
                    WaveOutCoreAudioDevice.AddDevices(devices);
                }
            }
            return devices;
        }

        public abstract void Open(WaveFormat waveFormat = null);
        public abstract void Play(Stream stream);
        public abstract void Play(byte[] buffer, int offset, int count);
        //public abstract void Pause();
        //public abstract void Restart();
        public abstract void Reset();
        public abstract void Close();

    }
}
