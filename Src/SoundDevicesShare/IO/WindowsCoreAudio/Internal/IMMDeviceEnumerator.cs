using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.WindowsCoreAudio.Internal
{
    [ComImport]
    [Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
    internal class MMDeviceEnumerator
    {
    }

    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMDeviceEnumerator
    {
        [PreserveSig]
        int EnumAudioEndpoints(EDataFlow dataFlow, DEVICE_STATE StateMask, out IMMDeviceCollection device);
        [PreserveSig]
        int GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, out IMMDevice ppEndpoint);
        //[PreserveSig]
        //int SetDefaultAudioEndpoint(IMMDevice ppEndpoint);
        [PreserveSig]
        int GetDevice(string pwstrId, out IMMDevice ppDevice);
        [PreserveSig]
        int RegisterEndpointNotificationCallback(IntPtr pClient);
        [PreserveSig]
        int UnregisterEndpointNotificationCallback(IntPtr pClient);
    }
}
