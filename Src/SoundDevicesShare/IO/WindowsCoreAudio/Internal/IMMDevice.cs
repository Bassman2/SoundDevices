using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.WindowsCoreAudio.Internal
{
    [Guid("D666063F-1587-4E43-81F1-B948E807363F"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMDevice
    {
        [PreserveSig]
        int Activate(ref Guid iid, CLSCTX dwClsCtx, IntPtr pActivationParams, [Out(), MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);
        [PreserveSig]
        int OpenPropertyStore(EStgmAccess stgmAccess, out IPropertyStore propertyStore);
        [PreserveSig]
        int GetId([Out(), MarshalAs(UnmanagedType.LPWStr)] out string ppstrId);
        [PreserveSig]
        int GetState(out DEVICE_STATE pdwState);
    }
}
