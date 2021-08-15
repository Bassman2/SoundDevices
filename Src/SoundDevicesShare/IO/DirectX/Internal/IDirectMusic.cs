using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.DirectX.Internal
{
    [ComImport]
    [Guid("6536115a-7b2d-11d2-ba18-0000f875ac12")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusic
    {
        /*  IDirectMusic */
        int EnumPort(int dwIndex, ref DMUS_PORTCAPS pPortCaps);
        void CreateMusicBuffer(ref DMUS_BUFFERDESC pBufferDesc, ref IDirectMusicBuffer ppBuffer, IntPtr pUnkOuter);
        void CreatePort(ref Guid rclsidPort, ref DMUS_PORTPARAMS pPortParams, ref IDirectMusicPort ppPort, IntPtr pUnkOuter);
        void EnumMasterClock(int dwIndex, ref DMUS_CLOCKINFO lpClockInfo);
        void GetMasterClock(ref Guid pguidClock, ref IReferenceClock ppReferenceClock);
        void SetMasterClock(ref Guid rguidClock);
        void Activate(int fEnable);
        void GetDefaultPort(ref Guid pguidPort);
        void SetDirectSound(ref IDirectSound pDirectSound, IntPtr hWnd);
    }
}
