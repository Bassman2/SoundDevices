using SoundDevices.IO.DirectX.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.DirectX
{
    public sealed class MidiOutDirectXDevice : MidiOutDevice
    {
        internal static void AddDevices(SoundDeviceType soundDeviceType, List<MidiOutDevice> devices)
        {
            DirectMusicImport.AddOutDevices(soundDeviceType, devices);
        }

        internal MidiOutDirectXDevice(Guid deviceId, string name, string description)
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

        public override void Send(int msg)
        { }

        public override void Send(byte[] data)
        { }
    }
}
