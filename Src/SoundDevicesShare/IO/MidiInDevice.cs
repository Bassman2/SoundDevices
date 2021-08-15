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
        public static IEnumerable<MidiInDevice> GetDevices(SoundDeviceType soundDeviceType = SoundDeviceType.All)
        {
            List<MidiInDevice> devices = new();
            if (OperatingSystem.IsWindows())
            {
                if (soundDeviceType.HasFlag(SoundDeviceType.WinMM))
                {
                    MidiInWinMMDevice.AddDevices(soundDeviceType, devices);
                }
                if ((soundDeviceType & SoundDeviceType.DirectX) != 0)
                {
                    MidiInDirectXDevice.AddDevices(soundDeviceType, devices);
                }
            }
            if (OperatingSystem.IsLinux())
            {
                if (soundDeviceType.HasFlag(SoundDeviceType.ALSA))
                {
                    MidiInALSADevice.AddDevices(soundDeviceType, devices);
                }
            }
            if (OperatingSystem.IsMacOS())
            {
                if (soundDeviceType.HasFlag(SoundDeviceType.CoreAudio))
                {
                    MidiInCoreAudioDevice.AddDevices(soundDeviceType, devices);
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
