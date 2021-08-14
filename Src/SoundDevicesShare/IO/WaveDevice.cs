using SoundDevices.IO.ASIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SoundDevices.IO
{
    public abstract class WaveDevice : SoundDevice
    {
        public static IEnumerable<WaveDevice> GetDevices(SoundDeviceType soundDeviceTypes = SoundDeviceType.All)
        {
            List<WaveDevice> devices = new();
            if (OperatingSystem.IsWindows())
            {
                if (soundDeviceTypes.HasFlag(SoundDeviceType.ASIO))
                {
                    WaveASIODevice.AddDevices(devices);
                }
            }
            
            return devices;
        }

        public abstract void Open(WaveFormat waveFormat = null);
        //public abstract void Play(Stream stream);
        //public abstract void Play(byte[] buffer, int offset, int count);
        //public abstract void Pause();
        //public abstract void Restart();
        public abstract void Start();
        public abstract void Stop();
        public abstract void Reset();
        public abstract void Close();
    }
}
