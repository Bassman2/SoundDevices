using SoundDevices.IO.DirectX;
using SoundDevices.IO.WinMM;
using SoundDevices.IO.ASIO;
using SoundDevices.IO.CoreAudio;
using SoundDevices.IO.ALSA;
using System;
using System.Collections.Generic;
using System.IO;
using SoundDevices.IO.WindowsCoreAudio;

namespace SoundDevices.IO
{
    public abstract class WaveOutDevice : SoundDevice
    {
        public static IEnumerable<WaveOutDevice> GetDevices(SoundDeviceType soundDeviceType = SoundDeviceType.All)
        {
            List<WaveOutDevice> devices = new();
            if (OperatingSystem.IsWindows())
            {
                if (soundDeviceType.HasFlag(SoundDeviceType.WinCoreAudio))
                {
                    WaveOutWinCoreAudioDevice.AddDevices(soundDeviceType, devices);
                }
                if (soundDeviceType.HasFlag(SoundDeviceType.WinMM))
                {
                    WaveOutWinMMDevice.AddDevices(soundDeviceType, devices);
                }
                if ((soundDeviceType & SoundDeviceType.DirectX) != 0)
                {
                    WaveOutDirectXDevice.AddDevices(soundDeviceType, devices);
                }
                //if (soundDeviceTypes.HasFlag(SoundDeviceType.ASIO))
                //{
                //    WaveOutASIODevice.AddDevices(soundDeviceTypes, devices);
                //}
            }
            if (OperatingSystem.IsLinux())
            {
                if (soundDeviceType.HasFlag(SoundDeviceType.ALSA))
                {
                    WaveOutALSADevice.AddDevices(soundDeviceType, devices);
                }
            }
            if (OperatingSystem.IsMacOS())
            {
                if (soundDeviceType.HasFlag(SoundDeviceType.CoreAudio))
                {
                    WaveOutCoreAudioDevice.AddDevices(soundDeviceType, devices);
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
