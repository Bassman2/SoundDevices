using MediaDevices.IO.Internal.ASIO;
using MediaDevices.IO.Internal.DirectSound;
using MediaDevices.IO.Internal.WinMM;
using System;
using System.Collections.Generic;

namespace MediaDevices.IO.Wave
{
    public class WaveInDevice : SoundDevice
    {
        public static WaveInDevice[] GetDevices(WaveDeviceTypes waveDeviceTypes)
        {
            List<WaveInDevice> devices = new();
            if (OperatingSystem.IsWindows())
            {
                if (waveDeviceTypes.HasFlag(WaveDeviceTypes.WinMM))
                {
                    WaveInWinMMDevice.AddInternalDevices(devices);
                }
                if (waveDeviceTypes.HasFlag(WaveDeviceTypes.ASIO))
                {
                    WaveInASIODevice.AddInternalDevices(devices);
                }
                if (waveDeviceTypes.HasFlag(WaveDeviceTypes.DirectSound))
                {
                    WaveInDirectSoundDevice.AddInternalDevices(devices);
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
