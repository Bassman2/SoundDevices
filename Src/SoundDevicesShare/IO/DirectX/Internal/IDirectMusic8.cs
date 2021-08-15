using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.DirectX.Internal
{
    [ComImport]
    [Guid("2d3629f7-813d-4939-8508-f05c6b75fd97")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusic8 : IDirectMusic
    {
        // IDirectMusic8 
        public void SetExternalMasterClock(ref IReferenceClock pClock);
    }
}
