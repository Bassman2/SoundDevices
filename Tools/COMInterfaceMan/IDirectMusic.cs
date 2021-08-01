using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    //https://docs.microsoft.com/de-de/dotnet/standard/native-interop/type-marshaling
    //https://docs.microsoft.com/en-us/previous-versions/ms808940(v=msdn.10)
    // https://docs.microsoft.com/en-us/previous-versions/ms811324(v=msdn.10)
    //https://docs.microsoft.com/en-us/previous-versions/ms811324(v=msdn.10)?source=docs

    //C:\Program Files(x86)\Windows Kits\10\Include\10.0.18362.0\um\dmusicc.h

    [ComImport]
    [Guid("636b9f10-0c7d-11d1-95b2-0020afdc7421")]
    internal class DirectMusic //: IDirectMusic
    {
    }

    [ComImport]
    [Guid("6536115a-7b2d-11d2-ba18-0000f875ac12")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusic
    {
        void EnumPort(uint dwIndex, out DMUS_PORTCAPS pPortCaps);
        void CreateMusicBuffer(ref DMUS_BUFFERDESC pBufferDesc, out IDirectMusicBuffer ppBuffer, out IntPtr pUnkOuter);
        //void CreatePort(ref Guid rclsidPort, ref DMUS_PORTPARAMS pPortParams, LPDIRECTMUSICPORT* ppPort, out IntPtr pUnkOuter);
        //void EnumMasterClock(uint dwIndex, LPDMUS_CLOCKINFO lpClockInfo);
        void GetMasterClock(ref Guid pguidClock, out IReferenceClock ppReferenceClock);
        void SetMasterClock(ref Guid rguidClock);
        void Activate(bool fEnable);
        void GetDefaultPort(out Guid pguidPort);
        void SetDirectSound(/*LPDIRECTSOUND*/ IntPtr pDirectSound, IntPtr hWnd);

    }
}
