using SoundDevices.IO;
using System.Collections.Generic;
using System.Linq;

namespace ShowDevices.ViewModel
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            this.MidiDevices = MidiDevice.GetDevices().Select(i => new DeviceViewModel(i)).ToList();
            this.MidiInDevices = MidiInDevice.GetDevices().Select(i => new DeviceViewModel(i)).ToList();
            this.MidiOutDevices = MidiOutDevice.GetDevices().Select(i => new DeviceViewModel(i)).ToList();
            this.WaveDevices = WaveDevice.GetDevices().Select(i => new DeviceViewModel(i)).ToList();
            this.WaveInDevices = WaveInDevice.GetDevices().Select(i => new DeviceViewModel(i)).ToList();
            this.WaveOutDevices = WaveOutDevice.GetDevices().Select(i => new DeviceViewModel(i)).ToList();
        }

        public List<DeviceViewModel> MidiDevices { get; }

        public List<DeviceViewModel> MidiInDevices { get; }     
        
        public List<DeviceViewModel> MidiOutDevices { get; }

        public List<DeviceViewModel> WaveDevices { get; }

        public List<DeviceViewModel> WaveInDevices { get; }

        public List<DeviceViewModel> WaveOutDevices { get; }
    }
}
