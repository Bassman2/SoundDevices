﻿using SoundDevices.IO.ALSA;
using SoundDevices.IO.CoreMIDI;
using SoundDevices.IO.DirectX;
using SoundDevices.IO.WinMM;
using System;
using System.Collections.Generic;

namespace SoundDevices.IO
{
    public abstract class MidiOutDevice : SoundDevice
    {
        public static IEnumerable<MidiOutDevice> GetDevices(SoundDeviceType soundDeviceTypes = SoundDeviceType.All)
        {
            List<MidiOutDevice> devices = new();
            if (OperatingSystem.IsWindows())
            {
                if (soundDeviceTypes.HasFlag(SoundDeviceType.WinMM))
                {
                    MidiOutWinMMDevice.AddDevices(devices);
                }
                if (soundDeviceTypes.HasFlag(SoundDeviceType.DirectX))
                {
                    MidiOutDirectXDevice.AddDevices(devices);
                }
            }
            if (OperatingSystem.IsLinux())
            {
                if (soundDeviceTypes.HasFlag(SoundDeviceType.ALSA))
                {
                    MidiOutALSADevice.AddDevices(devices);
                }
            }
            if (OperatingSystem.IsMacOS())
            {
                if (soundDeviceTypes.HasFlag(SoundDeviceType.CoreAudio))
                {
                    MidiOutCoreAudioDevice.AddDevices(devices);
                }
            }
            return devices.ToArray();
        }

        public abstract void Open();
        public abstract void Send(int msg);
        public abstract void Send(byte[] data);
        public abstract void Reset();
        public abstract void Close();

    }
}
