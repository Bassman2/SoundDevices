﻿using MediaDevices.IO;
using MediaDevices.IO.MIDI;
using MediaDevices.IO.Wave;
using System;

namespace ShowDevices.ViewModel
{
    public class DeviceViewModel
    {
        public DeviceViewModel(SoundDevice device)
        {
            this.InterfaceType = device.InterfaceType;
            this.Name = device.Name;
            this.Description = device.Description;
            this.Version = device.Version;
        }

        public DeviceViewModel(MidiInDevice device) : this((SoundDevice) device)
        {
            
        }

        public DeviceViewModel(MidiOutDevice device) : this((SoundDevice)device)
        {
            
        }

        public DeviceViewModel(WaveInDevice device) : this((SoundDevice)device)
        {
            
        }

        public DeviceViewModel(WaveOutDevice device) : this((SoundDevice)device)
        {
            
        }
        
        public string Name { get; }

        public Version Version { get; }

        public SoundInterfaceType InterfaceType { get; }

        public string Description { get; }

    }
}