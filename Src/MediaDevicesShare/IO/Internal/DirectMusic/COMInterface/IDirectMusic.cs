using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [ComImport]
    [Guid("636b9f10-0c7d-11d1-95b2-0020afdc7421")]
    internal class DirectMusic
    {
    }

    [ComImport]
    [Guid("6536115a-7b2d-11d2-ba18-0000f875ac12")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusic
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

    }
}
