using SoundDevices.DirectX;
using SoundDevices.WinMM;
using SoundDevices.ASIO;
using SoundDevices.CoreMIDI;
using SoundDevices.ALSA;
using System;
using System.Collections.Generic;

namespace SoundDevices
{
    public class WaveOutDevice : SoundDevice
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
                if (soundDeviceTypes.HasFlag(SoundDeviceType.ASIO))
                {
                    WaveOutASIODevice.AddDevices(devices);
                }
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
                if (soundDeviceTypes.HasFlag(SoundDeviceType.CoreMIDI))
                {
                    WaveOutCoreMIDIDevice.AddDevices(devices);
                }
            }
            return devices;
        }
  
    }
}
