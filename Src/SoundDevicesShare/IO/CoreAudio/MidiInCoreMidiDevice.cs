using SoundDevices.IO.CoreAudio.Internal;
using System;
using System.Collections.Generic;
using System.Runtime.Versioning;

namespace SoundDevices.IO.CoreAudio
{
    [SupportedOSPlatform("MacOS")]
    public sealed class MidiInCoreMidiDevice : MidiInDevice
    {
        private IntPtr deviceHandle;

        internal static void AddDevices(SoundDeviceType soundDeviceType, List<MidiInDevice> devices)
        {
            int num = CoreMidiImport.MIDIGetNumberOfDevices();
            for (int i = 0; i < num; i++)
            {
                IntPtr deviceHandle = CoreMidiImport.MIDIGetDevice(i);
                devices.Add(new MidiInCoreMidiDevice(deviceHandle));
            }

            num = CoreMidiImport.MIDIGetNumberOfExternalDevices();
            for (int i = 0; i < num; i++)
            {
                IntPtr deviceHandle = CoreMidiImport.MIDIGetExternalDevice(i);
                devices.Add(new MidiInCoreMidiDevice(deviceHandle));
            }

            num = CoreMidiImport.MIDIGetNumberOfDestinations();
            for (int i = 0; i < num; i++)
            {

            }

            num = CoreMidiImport.MIDIGetNumberOfSources();
            for (int i = 0; i < num; i++)
            {

            }
        }

        private MidiInCoreMidiDevice(IntPtr deviceHandle)
        {
            this.deviceHandle = deviceHandle;
            this.DeviceType = SoundDeviceType.CoreAudio;
            this.DeviceId = new Guid(CoreMidiImport.MidiObjectGetUniqueID(this.deviceHandle));
            this.Name = CoreMidiImport.MidiObjectGetName(this.deviceHandle);
            this.Description = CoreMidiImport.MidiObjectGetDisplayName(this.deviceHandle) + " " + CoreMidiImport.MidiObjectGetModel(this.deviceHandle);
            this.Version = new Version(CoreMidiImport.MidiObjectGetDriverVersion(this.deviceHandle), 0);
            this.Manufacturer = CoreMidiImport.MidiObjectGetManufacturer(this.deviceHandle);
        }

        public override void Dispose()
        {
            if (this.deviceHandle != IntPtr.Zero)
            {
                this.deviceHandle = IntPtr.Zero;
            }
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
