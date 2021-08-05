﻿using SoundDevices.ALSA;
using SoundDevices.CoreMIDI;
using SoundDevices.DirectX;
using SoundDevices.WinMM;
using System;
using System.Collections.Generic;

namespace SoundDevices
{
    public class MidiOutDevice : SoundDevice
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
                if (soundDeviceTypes.HasFlag(SoundDeviceType.CoreMIDI))
                {
                    MidiOutCoreMIDIDevice.AddDevices(devices);
                }
            }
            return devices.ToArray();
        }
    }
}
