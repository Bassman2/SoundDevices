using SoundDevices.IO.DirectX.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.DirectX
{
    public sealed class MidiInDirectXDevice : MidiInDevice
    {
        internal static void AddDevices(SoundDeviceType soundDeviceType, List<MidiInDevice> devices)
        {
            DirectMusicImport.AddInDevices(soundDeviceType, devices);
        }

        internal MidiInDirectXDevice(Guid deviceId, string name, string description)
        {
            this.DeviceType = SoundDeviceType.DirectX;
            this.DeviceId = deviceId;
            this.Name = name;
            this.Description = description;
        }

        public override void Dispose()
        { }

        public override void Open()
        { }

        public override void Close()
        { }

        public override void Reset()
        { }

        public override void Start()
        { }

        public override void Stop()
        { }
    }
}
