using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.WindowsCoreAudio.Internal
{
    [Guid("7991EEC9-7E89-4D85-8390-6C703CEC60C0")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMNotificationClient
    {
        [PreserveSig]
        void OnDeviceStateChanged([In, MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId, DEVICE_STATE dwNewState);
        [PreserveSig]
        void OnDeviceAdded([In, MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId);
        [PreserveSig]
        void OnDeviceRemoved([In, MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId);
        [PreserveSig]
        void OnDefaultDeviceChanged(EDataFlow flow, ERole role, [In, MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId);
        [PreserveSig]
        void OnPropertyValueChanged([In, MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId, PROPERTYKEY key);
    };
}
