using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.WindowsCoreAudio.Internal
{
    [Guid("886d8eeb-8cf2-4446-8d02-cdba1dbdcf99"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPropertyStore
    {
        [PreserveSig]
        int GetCount(out Int32 count);
        [PreserveSig]
        int GetAt(int iProp, out PROPERTYKEY pkey);
        [PreserveSig]
        int GetValue(ref PROPERTYKEY key, out PropVariant pv);
        [PreserveSig]
        int SetValue(ref PROPERTYKEY key, ref PropVariant propvar);
        [PreserveSig]
        int Commit();
    };
}
