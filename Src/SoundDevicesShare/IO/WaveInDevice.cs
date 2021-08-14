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
        public static IEnumerable<WaveInDevice> GetDevices(SoundDeviceType soundDeviceTypes = SoundDeviceType.All)
        {
            List<WaveInDevice> devices = new();
            if (OperatingSystem.IsWindows())
            {
                if (soundDeviceTypes.HasFlag(SoundDeviceType.WinMM))
                {
                    WaveInWinMMDevice.AddDevices(devices);
                }
                if (soundDeviceTypes.HasFlag(SoundDeviceType.DirectX))
                {
                    WaveInDirectXDevice.AddDevices(devices);
                }
                if (soundDeviceTypes.HasFlag(SoundDeviceType.ASIO))
                {
                    WaveInASIODevice.AddDevices(devices);
                }
            }
            if (OperatingSystem.IsLinux())
            {
                if (soundDeviceTypes.HasFlag(SoundDeviceType.ALSA))
                {
                    WaveInALSADevice.AddDevices(devices);
                }
            }
            if (OperatingSystem.IsMacOS())
            {
                if (soundDeviceTypes.HasFlag(SoundDeviceType.CoreMIDI))
                {
                    WaveInCoreMIDIDevice.AddDevices(devices);
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
