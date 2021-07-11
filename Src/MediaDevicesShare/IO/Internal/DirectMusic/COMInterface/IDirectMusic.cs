using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [ComImport]
    [Guid("636b9f10-0c7d-11d1-95b2-0020afdc7421")]
    internal class DirectMusic : IDirectMusic
    {
    }

    [ComImport]
    [Guid("6536115a-7b2d-11d2-ba18-0000f875ac12")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusic
    {

        /*  IDirectMusic */
        /*
        int EnumPort(DWORD dwIndex, LPDMUS_PORTCAPS pPortCaps);
        int CreateMusicBuffer(LPDMUS_BUFFERDESC pBufferDesc, LPDIRECTMUSICBUFFER* ppBuffer, LPUNKNOWN pUnkOuter);
        int CreatePort(REFCLSID rclsidPort, LPDMUS_PORTPARAMS pPortParams, LPDIRECTMUSICPORT* ppPort, LPUNKNOWN pUnkOuter);
        int EnumMasterClock(DWORD dwIndex, LPDMUS_CLOCKINFO lpClockInfo);
        int GetMasterClock(LPGUID pguidClock, IReferenceClock **ppReferenceClock);
        int SetMasterClock(REFGUID rguidClock);
        int Activate(BOOL fEnable);
        int GetDefaultPort(LPGUID pguidPort);
        int SetDirectSound(LPDIRECTSOUND pDirectSound, HWND hWnd);
            */


    }
}
