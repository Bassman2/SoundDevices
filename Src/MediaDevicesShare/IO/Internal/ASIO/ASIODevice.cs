using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace MediaDevices.IO.Internal.ASIO
{

    // https://devblogs.microsoft.com/dotnet/improvements-in-native-code-interop-in-net-5-0/

    
    internal class ASIODevice
    {
        private Type comType;
        private object driver;

        [SupportedOSPlatform("Windows")]
        public void CreateDevice(Guid guid)
        {
            comType = Type.GetTypeFromCLSID(guid);
            driver = Activator.CreateInstance(comType);

            // InvokeMember(string name, BindingFlags invokeAttr, Binder? binder, object? target, object?[]? args);
            //Type.InvokeMember()
        }

        
        //public string GetDriverName()
        //{

        //    // InvokeMember(string name, BindingFlags invokeAttr, Binder? binder, object? target, object?[]? args);
        //    comType.InvokeMember("getDriverName", BindingFlags.InvokeMethod, null, driver, new object[] { });
        //}
        

        public static class NativeMethods
        {
            [DllImport("ole32.dll", CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
            private static extern int CoInitializeEx(
                IntPtr pvReserved,
                uint dwCoInit);

            [DllImport("ole32.Dll")]
            private static extern int CoCreateInstance(
                ref Guid clsid,
                IntPtr inner,
                uint context,
                ref Guid uuid,
                out IntPtr rReturnedComObject);
        }
    }
}
