using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [ComImport]
    [Guid("636b9f10-0c7d-11d1-95b2-0020afdc7421")]
    internal class DirectMusic8 : IDirectMusic8
    {
    }

    [ComImport]
    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusic8
    {

        /*  IDirectMusic 
        STDMETHOD(EnumPort)             (THIS_ DWORD dwIndex,
                                               LPDMUS_PORTCAPS pPortCaps) PURE;
    STDMETHOD(CreateMusicBuffer)    (THIS_ LPDMUS_BUFFERDESC pBufferDesc,
                                           LPDIRECTMUSICBUFFER *ppBuffer,
                                           LPUNKNOWN pUnkOuter) PURE;
    STDMETHOD(CreatePort)           (THIS_ REFCLSID rclsidPort,
                                           LPDMUS_PORTPARAMS pPortParams,
                                           LPDIRECTMUSICPORT* ppPort,
                                           LPUNKNOWN pUnkOuter) PURE;
    STDMETHOD(EnumMasterClock)      (THIS_ DWORD dwIndex,
                                           LPDMUS_CLOCKINFO lpClockInfo) PURE;
    STDMETHOD(GetMasterClock)       (THIS_ LPGUID pguidClock,
                                           IReferenceClock **ppReferenceClock) PURE;
    STDMETHOD(SetMasterClock)       (THIS_ REFGUID rguidClock) PURE;
    STDMETHOD(Activate)             (THIS_ BOOL fEnable) PURE;
    STDMETHOD(GetDefaultPort)       (THIS_ LPGUID pguidPort) PURE;
    STDMETHOD(SetDirectSound)       (THIS_ LPDIRECTSOUND pDirectSound,
                      /*  IDirectMusic8 
        STDMETHOD(SetExternalMasterClock)
                                    (THIS_ IReferenceClock * pClock) PURE;                     HWND hWnd) PURE;
        */


    }
}
