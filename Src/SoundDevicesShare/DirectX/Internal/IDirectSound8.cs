using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.DirectX.Internal
{
    // C:\Program Files (x86)\Windows Kits\10\Include\10.0.18362.0\um\dsound.h

    [ComImport]
    [Guid("3901CC3F-84B5-4FA4-BA35-AA8172B8A09B")]
    internal class DirectSound8 : IDirectSound8
    {
    }

    [ComImport]
    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectSound8
    {
    }
}
