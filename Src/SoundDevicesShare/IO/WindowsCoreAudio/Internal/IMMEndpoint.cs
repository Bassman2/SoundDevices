using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.WindowsCoreAudio.Internal
{
    [Guid("1BE09788-6894-4089-8586-9A2A6C265AC5")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMEndpoint
    {
        [PreserveSig]
        int GetDataFlow(out EDataFlow pDataFlow);
    };
}
