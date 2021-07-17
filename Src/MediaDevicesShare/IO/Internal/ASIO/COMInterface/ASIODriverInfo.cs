using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaDevices.IO.Internal.ASIO.COMInterface
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct ASIODriverInfo
    {
        long asioVersion;
        long driverVersion;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        string name;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 124)]
        string errorMessage;
        IntPtr sysRef;
    }
}
