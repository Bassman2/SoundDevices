using SoundDevices.IO.ALSA;
using SoundDevices.IO.CoreMIDI;
using SoundDevices.IO.DirectX;
using SoundDevices.IO.WinMM;
using System;
using System.Collections.Generic;

namespace SoundDevices.IO
{
    public abstract class MidiInDevice : SoundDevice
    {
        public static IEnumerable<MidiInDevice> GetDevices(SoundDeviceType soundDeviceTypes = SoundDeviceType.All)
        {
            List<MidiInDevice> devices = new();
            if (OperatingSystem.IsWindows())
            {
                if (soundDeviceTypes.HasFlag(SoundDeviceType.WinMM))
                {
                    MidiInWinMMDevice.AddDevices(devices);
                }
                if (soundDeviceTypes.HasFlag(SoundDeviceType.DirectX))
                {
                    MidiInDirectXDevice.AddDevices(devices);
                }
            }
            if (OperatingSystem.IsLinux())
            {
                if (soundDeviceTypes.HasFlag(SoundDeviceType.ALSA))
                {
                    MidiInALSADevice.AddDevices(devices);
                }
            }
            if (OperatingSystem.IsMacOS())
            {
                if (soundDeviceTypes.HasFlag(SoundDeviceType.CoreMIDI))
                {
                    MidiInCoreMIDIDevice.AddDevices(devices);
                }
            }
            return devices;
        }

        public event EventHandler<MidiMsgEventArgs> MidiMsgReceived;

        protected void RaiseMidiMsgReceived(int midiMsg)
        {
            this.MidiMsgReceived?.Invoke(this, new MidiMsgEventArgs(midiMsg));
        }

        public abstract void Open();
        public abstract void Start();
        public abstract void Stop(); 
        public abstract void Reset();
        public abstract void Close();

    }
}
