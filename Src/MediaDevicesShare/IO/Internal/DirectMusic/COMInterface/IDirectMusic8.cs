using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [ComImport]
    [Guid("2d3629f7-813d-4939-8508-f05c6b75fd97")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusic8
    {
        void EnumPort(
            uint dwIndex,
            ref DMUS_PORTCAPS pPortCaps);

        void CreateMusicBuffer(
            ref DMUS_BUFFERDESC pBufferDesc,
            ref IDirectMusicBuffer ppBuffer,
            IntPtr pUnkOuter);

        void CreatePort(
            ref Guid rclsidPort,
            ref DMUS_PORTPARAMS pPortParams,
            ref IDirectMusicPort ppPort,
            IntPtr pUnkOuter);

        void EnumMasterClock(
            uint dwIndex,
            ref DMUS_CLOCKINFO lpClockInfo);

        void GetMasterClock(
            ref Guid pguidClock,
            IReferenceClock ppReferenceClock);

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

        void SetExternalMasterClock(
            IReferenceClock pClock);

    }
}
