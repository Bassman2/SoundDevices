using System;
using System.Diagnostics;

namespace SoundDevices.IO.WindowsCoreAudio.Internal
{
    internal class PropertyStore
    {
        private IPropertyStore propertyStore;

        public PropertyStore(IMMDevice device)
        {
            device.OpenPropertyStore(EStgmAccess.STGM_READ, out this.propertyStore);
        }

        private string GetPropertyString(PROPERTYKEY key)
        {
            this.propertyStore.GetValue(ref key, out PropVariant propVariant);
            return propVariant.ToString();
        }

        private Version GetPropertyVersion(PROPERTYKEY key)
        {
            this.propertyStore.GetValue(ref key, out PropVariant propVariant);
            return propVariant.IsEmpty ? new Version() : new Version(propVariant, 0);
        }

        public string GetDeviceManufacturer => GetPropertyString(PKEY.PKEY_Device_Manufacturer);
        public string GetDeviceDesc => GetPropertyString(PKEY.PKEY_Device_DeviceDesc);
        public string GetDeviceDevType => GetPropertyString(PKEY.PKEY_Device_DevType);
        public string GetDeviceFriendlyName => GetPropertyString(PKEY.PKEY_Device_FriendlyName);
        public Version GetDeviceDriverVersion => GetPropertyVersion(PKEY.PKEY_Device_DriverVersion);
        public string GetDeviceInterface_FriendlyName => GetPropertyString(PKEY.PKEY_DeviceInterface_FriendlyName);

        public void DebugProperties()
        {
            this.propertyStore.GetCount(out int num);
            for (int i = 0; i < num; i++)
            {
                this.propertyStore.GetAt(i, out PROPERTYKEY key);
                this.propertyStore.GetValue(ref key, out PropVariant pv);
                Debug.WriteLine($"{key} {pv}");
            }
        }
    }
}
