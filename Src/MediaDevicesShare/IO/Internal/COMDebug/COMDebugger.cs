using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace MediaDevices.IO.Internal.COMDebug
{
    internal static class COMDebugger
    {

        public static void Write(Type comType)
        {
            bool isCOM = comType.IsCOMObject;

            TypeInfo ti = comType.GetTypeInfo();
            Debug.WriteLine(ti.Name);
            foreach (var m in ti.DeclaredMethods)
            {
                string p = m.ToString();
                string n = m.Name;
                Debug.WriteLine(m.Name);
            }
        }
    }
}
