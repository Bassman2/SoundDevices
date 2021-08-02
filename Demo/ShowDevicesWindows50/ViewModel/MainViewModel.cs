using MediaDevices.IO.MIDI;
using MediaDevices.IO.Wave;
using System.Collections.Generic;
using System.Linq;

namespace ShowDevices.ViewModel
{
    public class MainViewModel
    {
        public MainViewModel()
        {

            this.MidiInDevices = MidiInDevice.GetDevices(MidiDeviceTypes.All).Select(i => new DeviceViewModel(i)).ToList();
            this.MidiOutDevices = MidiOutDevice.GetDevices(MidiDeviceTypes.All).Select(i => new DeviceViewModel(i)).ToList();
            this.WaveInDevices = WaveInDevice.GetDevices(WaveDeviceTypes.All).Select(i => new DeviceViewModel(i)).ToList();
            this.WaveOutDevices = WaveOutDevice.GetDevices(WaveDeviceTypes.All).Select(i => new DeviceViewModel(i)).ToList();
        }

        public List<DeviceViewModel> MidiInDevices { get; }     
        
        public List<DeviceViewModel> MidiOutDevices { get; }

        public List<DeviceViewModel> WaveInDevices { get; }

        public List<DeviceViewModel> WaveOutDevices { get; }
    }
}
