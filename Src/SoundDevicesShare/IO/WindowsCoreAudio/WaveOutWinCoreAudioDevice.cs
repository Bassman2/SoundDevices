using SoundDevices.IO.WindowsCoreAudio.Internal;
using System.Collections.Generic;
using System.IO;

namespace SoundDevices.IO.WindowsCoreAudio
{

    internal sealed class WaveOutWinCoreAudioDevice : WaveOutDevice
    {
        private IMMDevice device;
        private readonly PropertyStore propertyStore;

        internal static void AddDevices(SoundDeviceType soundDeviceType, List<WaveOutDevice> devices)
        {
            IMMDeviceEnumerator devEnum = new MMDeviceEnumerator() as IMMDeviceEnumerator;
            devEnum.EnumAudioEndpoints(EDataFlow.eRender, DEVICE_STATE.DEVICE_STATEMASK_ALL, out IMMDeviceCollection col);
            col.GetCount(out uint num);
            for (uint i = 0; i < num; i++)
            {
                col.Item(i, out IMMDevice device);
                devices.Add(new WaveOutWinCoreAudioDevice(device));
            }
        }

        private WaveOutWinCoreAudioDevice(IMMDevice device)
        {
            this.device = device;
            this.DeviceType = SoundDeviceType.WinCoreAudio;

            this.propertyStore = new PropertyStore(device);
            this.propertyStore.DebugProperties();

            this.Manufacturer = propertyStore.GetDeviceManufacturer;
            this.Name = propertyStore.GetDeviceFriendlyName;
            this.Description = propertyStore.GetDeviceDesc;
            this.Version = propertyStore.GetDeviceDriverVersion;
        }

        public override void Dispose()
        {
            if (this.device != null)
            {
                this.device = null;
            }
        }

        public override void Open(WaveFormat waveFormat = null)
        { }

        public override void Play(Stream stream)
        {

        }

        public override void Play(byte[] buffer, int offset, int count)
        {

        }

        public override void Reset()
        { }

        public override void Close()
        { }
    }
}
