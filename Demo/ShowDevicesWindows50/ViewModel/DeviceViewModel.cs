using SoundDevices.IO;
using System;

namespace ShowDevices.ViewModel
{
    public class DeviceViewModel
    {
        public DeviceViewModel(SoundDevice device)
        {
            this.Type = device.DeviceType;
            this.Name = device.Name;
            this.DeviceId = device.DeviceId;
            this.Description = device.Description;
            this.Version = device.Version;
            this.Manufacturer = device.Manufacturer;
        }

        public SoundDeviceType Type { get; }

        public string Name { get; }

        public Guid DeviceId { get; }

        public Version Version { get; }

        public string Description { get; }

        public string Manufacturer { get; }

    }
}
