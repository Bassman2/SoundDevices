using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [ComImport]
    [Guid("d2ac2878-b39b-11d1-8704-00600893b1bd")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusic
    {
        void EnumPort(
            uint dwIndex,
            ref DMUS_PORTCAPS pPortCaps);

        void CreateMusicBuffer(
            ref DMUS_BUFFERDESC pBufferDesc,
            ppBuffe r,
            IntPtr pUnkOuter);

        void CreatePort(
            ref Guid rclsidPort,
            ref DMUS_PORTPARAMS pPortParams,
            ppPor t,
            IntPtr pUnkOuter);

        void EnumMasterClock(
            uint dwIndex,
            ref DMUS_CLOCKINFO lpClockInfo);

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
            ref DIRECTSOUND pDirectSound,
            IntPtr hWnd);

    }
}
