using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [ComImport]
    [Guid("d2ac2878-b39b-11d1-8704-00600893b1bd")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusic8
    {
        void EnumPort(
            uint dwIndex,
            LPDMUS_PORTCAPS pPortCaps);

        void CreateMusicBuffer(
            LPDMUS_BUFFERDESC pBufferDesc,
            ppBuffe r,
            LPUNKNOWN pUnkOuter);

        void CreatePort(
            REFCLSID rclsidPort,
            LPDMUS_PORTPARAMS pPortParams,
            ppPor t,
            LPUNKNOWN pUnkOuter);

        void EnumMasterClock(
            uint dwIndex,
            LPDMUS_CLOCKINFO lpClockInfo);

        void GetMasterClock(
            ref Guid pguidClock,
            ppReferenceCloc k);

        void SetMasterClock(
            ref Guid rguidClock);

        void Activate(
            [MarshalAs(UnmanagedType.Bool)]
            bool fEnable);

        void GetDefaultPort(
            ref Guid pguidPort);

        void SetDirectSound(
            LPDIRECTSOUND pDirectSound,
            HWND hWnd);

        void SetExternalMasterClock(
            pCloc k);

    }
}
