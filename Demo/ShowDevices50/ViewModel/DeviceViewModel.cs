using MediaDevices.IO.MIDI;
using MediaDevices.IO.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowDevices.ViewModel
{
    public class DeviceViewModel
    {
        public DeviceViewModel(MidiInDeviceInfo info)
        {
            this.Name = info.Name;
            this.Version = info.Version;
        }

        public DeviceViewModel(MidiOutDeviceInfo info)
        {
            this.Name = info.Name;
            this.Version = info.Version;
        }

        public DeviceViewModel(WaveInDeviceInfo info)
        {
            this.Name = info.Name;
            this.Version = info.Version;
        }

        public DeviceViewModel(WaveOutDeviceInfo info)
        {
            this.Name = info.Name;
            this.Version = info.Version;
        }
        
        public string Name { get; }

        public Version Version { get; }
    }
}
