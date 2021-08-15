using SoundDevices.IO.DirectX.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.DirectX
{
    public class MidiInDirectXDevice : MidiInDevice
    {
        internal static void AddDevices(List<MidiInDevice> devices)
        {
            DirectMusicImport dmi = new DirectMusicImport();
            dmi.Initialize();
            dmi.AddDevices(devices);
        }

        internal MidiInDirectXDevice(Guid deviceId, string name, string description)
        {
            this.DeviceType = SoundDeviceType.DirectX;
            this.DeviceId = deviceId;
            this.Name = name;
            this.Description = description;
        }

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
