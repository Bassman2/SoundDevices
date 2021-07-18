using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [ComImport]
    [Guid("d2ac287a-b39b-11d1-8704-00600893b1bd")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusicPortDownload
    {
        void GetBuffer(
            uint dwDLId,
            IDirectMusicDownload ppIDMDownload);

        void AllocateBuffer(
            uint dwSize,
            IDirectMusicDownload ppIDMDownload);

        void GetDLId(
            uint pdwStartDLId,
            uint dwCount);

        void GetAppend(
            uint pdwAppend);

        void Download(
            IDirectMusicDownload pIDMDownload);

        void Unload(
            IDirectMusicDownload pIDMDownload);

    }
}
