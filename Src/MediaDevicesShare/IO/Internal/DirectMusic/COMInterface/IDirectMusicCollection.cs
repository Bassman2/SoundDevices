using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [ComImport]
    [Guid("480ff4b0-28b2-11d1-bef7-00c04fbf8fef")]
    internal class DirectMusicCollection
    {
    }

    [ComImport]
    [Guid("d2ac287c-b39b-11d1-8704-00600893b1bd")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusicCollection
    {
        void GetInstrument(
            uint dwPatch,
            IDirectMusicInstrument ppInstrument);

        void EnumInstrument(
            uint dwIndex,
            uint pdwPatch,
            string pwszName,
            uint dwNameLen);

    }
}
