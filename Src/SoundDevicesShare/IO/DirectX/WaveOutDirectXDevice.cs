using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SoundDevices.IO.DirectX.Internal;

namespace SoundDevices.IO.DirectX
{
    internal sealed class WaveOutDirectXDevice : WaveOutDevice
    {
        internal static void AddDevices(SoundDeviceType soundDeviceType, List<WaveOutDevice> devices)
        {
            DirectSoundImport.GetWaveOutDevices(devices);
        }

        internal WaveOutDirectXDevice(Guid deviceId, string name, string description)
        {
            this.DeviceType = SoundDeviceType.DirectX;
            this.DeviceId = deviceId;
            this.Name = name;
            this.Description = description;
        
        }

        public override void Dispose()
        { }

        public override void Open(WaveFormat waveFormat = null)
        { }

        public override void Play(Stream stream)
        {

        }

        public override void Play(byte[] buffer, int offset, int count)
        {

        }

        public override void Reset()
        { }

        public override void Close()
        { }
    }
}
