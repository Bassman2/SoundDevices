using MediaDevices.IO.Internal.DirectSound;
using MediaDevices.IO.Internal.WinMM;
using MediaDevices.IO.Internal.ASIO;
using System;
using System.Collections.Generic;

namespace MediaDevices.IO.Wave
{
    public class WaveOutDevice : SoundDevice
    {
        public static WaveOutDevice[] GetDevices(WaveDeviceTypes waveDeviceTypes)
        {
            List<WaveOutDevice> devices = new();
            if (OperatingSystem.IsWindows())
            {
                if (waveDeviceTypes.HasFlag(WaveDeviceTypes.WinMM))
                {
                    WaveOutWinMMDevice.AddInternalDevices(devices);
                }
                if (waveDeviceTypes.HasFlag(WaveDeviceTypes.ASIO))
                {
                    WaveOutASIODevice.AddInternalDevices(devices);
                }
                if (waveDeviceTypes.HasFlag(WaveDeviceTypes.DirectSound))
                {
                    WaveOutDirectSoundDevice.AddInternalDevices(devices);
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
