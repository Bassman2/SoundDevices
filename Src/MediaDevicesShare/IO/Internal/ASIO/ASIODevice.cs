using MediaDevices.IO.Internal.ASIO.COMInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace MediaDevices.IO.Internal.ASIO
{

    // https://devblogs.microsoft.com/dotnet/improvements-in-native-code-interop-in-net-5-0/

    //https://bytes.com/topic/c-sharp/answers/632358-interop-asio-driver-com-object
    
    internal class ASIODevice
    {
        private Type comType;
        private dynamic driver;

        [SupportedOSPlatform("Windows")]
        public void CreateDevice(Guid guid)
        {
            try
            {
                //$myType = [Type]::GetTypeFromCLSID($guid)
                //$com = [Activator]::CreateInstance($myType)
                //$obj = [Marshal]::CreateWrapperOfType($com, [NGLINC.ISegmentCycle])



                comType = Type.GetTypeFromCLSID(guid);

                bool isCOM = comType.IsCOMObject;

                object obj = Activator.CreateInstance(comType);

                //object x = Marshal.CreateWrapperOfType(obj, typeof(IAsioDriver));

                IAsioDriver asioDrv = (IAsioDriver)obj;



                //MethodInfo miDisplayString = obj.GetType().GetMethod("ASIOInit");

                //RequireReference(obj, "obj");
                //Type result = GetType((IDispatchInfo)obj, throwIfNotFound);
                //return result;


                TypeInfo ti = comType.GetTypeInfo();
                Debug.WriteLine(ti.Name);
                foreach (var m in ti.DeclaredMethods)
                {
                    string p = m.ToString();
                    string n = m.Name;
                    Debug.WriteLine(m.Name);
                }

                driver = Activator.CreateInstance(comType);
                string s = driver.ToString();

                //Type dispatchType = DispatchUtility.GetType(fso, true);

                dynamic info;
                driver.ASIOInit(out info);

                driver.ASIOExit();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            //dynamic obj = Marshal.GetActiveObject("MyLibrary.Application");
            //obj.Quit();

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
