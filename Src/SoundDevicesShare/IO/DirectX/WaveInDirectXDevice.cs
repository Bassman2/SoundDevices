using SoundDevices.IO.DirectX.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.DirectX
{
    internal sealed class WaveInDirectXDevice : WaveInDevice
    {
        internal static void AddDevices(SoundDeviceType soundDeviceType, List<WaveInDevice> devices)
        {
            DirectSoundImport.GetWaveInDevices(devices);
        }

        internal WaveInDirectXDevice(Guid deviceId, string name, string description)
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

        public override void Start()
        { }

        public override void Stop()
        { }

        public override void Reset()
        { }

        public override void Close()
        { }
    }
}
