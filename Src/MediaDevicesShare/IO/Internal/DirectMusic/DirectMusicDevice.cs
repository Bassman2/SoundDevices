using MediaDevices.IO.Internal.COMDebug;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MediaDevices.IO.Internal.DirectMusic
{
    //https://docs.microsoft.com/de-de/dotnet/standard/native-interop/type-marshaling
    //https://docs.microsoft.com/en-us/previous-versions/ms808940(v=msdn.10)
    // https://docs.microsoft.com/en-us/previous-versions/ms811324(v=msdn.10)
    //https://docs.microsoft.com/en-us/previous-versions/ms811324(v=msdn.10)?source=docs

    //C:\Program Files(x86)\Windows Kits\10\Include\10.0.18362.0\um\dmusicc.h

    internal class DirectMusicDevice
    {

        public static void GetDevices()
        {
            //comType = Type.GetTypeFromCLSID(guid);
            try
            {
                Type comType = Type.GetTypeFromProgID("Microsoft.DirectMusic");

                COMDebugger.Write(comType);


                object obj = Activator.CreateInstance(comType);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
