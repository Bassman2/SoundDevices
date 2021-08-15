using SoundDevices.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShowDevices.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private SoundDeviceType deviceType = SoundDeviceType.All;

        public MainViewModel()
        {
            //this.MidiDevices = MidiDevice.GetDevices().Select(i => new DeviceViewModel(i)).ToList();
            //this.MidiInDevices = MidiInDevice.GetDevices().Select(i => new DeviceViewModel(i)).ToList();
            //this.MidiOutDevices = MidiOutDevice.GetDevices().Select(i => new DeviceViewModel(i)).ToList();
            //this.WaveDevices = WaveDevice.GetDevices().Select(i => new DeviceViewModel(i)).ToList();
            //this.WaveInDevices = WaveInDevice.GetDevices().Select(i => new DeviceViewModel(i)).ToList();
            //this.WaveOutDevices = WaveOutDevice.GetDevices().Select(i => new DeviceViewModel(i)).ToList();
            GetDevices(this.deviceType);
        }

        public void GetDevices(SoundDeviceType deviceType)
        {
            this.MidiDevices = MidiDevice.GetDevices(deviceType).Select(i => new DeviceViewModel(i)).ToList();
            this.MidiInDevices = MidiInDevice.GetDevices(deviceType).Select(i => new DeviceViewModel(i)).ToList();
            this.MidiOutDevices = MidiOutDevice.GetDevices(deviceType).Select(i => new DeviceViewModel(i)).ToList();
            this.WaveDevices = WaveDevice.GetDevices(deviceType).Select(i => new DeviceViewModel(i)).ToList();
            this.WaveInDevices = WaveInDevice.GetDevices(deviceType).Select(i => new DeviceViewModel(i)).ToList();
            this.WaveOutDevices = WaveOutDevice.GetDevices(deviceType).Select(i => new DeviceViewModel(i)).ToList();

            NotifyPropertyChanged(nameof(MidiDevices));
            NotifyPropertyChanged(nameof(MidiInDevices));
            NotifyPropertyChanged(nameof(MidiOutDevices));
            NotifyPropertyChanged(nameof(WaveDevices));
            NotifyPropertyChanged(nameof(WaveInDevices));
            NotifyPropertyChanged(nameof(WaveOutDevices));
        }

        public SoundDeviceType[] DeviceTypes { get { return Enum.GetValues<SoundDeviceType>(); } }

        public SoundDeviceType DeviceType 
        { 
            get { return this.deviceType; } 
            set { this.deviceType = value; GetDevices(this.deviceType); }
        }

        public List<DeviceViewModel> MidiDevices { get; private set; }

        public List<DeviceViewModel> MidiInDevices { get; private set; }

        public List<DeviceViewModel> MidiOutDevices { get; private set; }

        public List<DeviceViewModel> WaveDevices { get; private set; }

        public List<DeviceViewModel> WaveInDevices { get; private set; }

        public List<DeviceViewModel> WaveOutDevices { get; private set; }
    }
}
