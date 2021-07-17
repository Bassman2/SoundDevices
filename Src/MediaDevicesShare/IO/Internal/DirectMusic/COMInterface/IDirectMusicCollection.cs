using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [ComImport]
    [Guid("d2ac2878-b39b-11d1-8704-00600893b1bd")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusicCollection
    {
        void GetInstrument(
            uint dwPatch,
            ppInstrumen t);

        void EnumInstrument(
            uint dwIndex,
            pdwPatc h,
            string pwszName,
            uint dwNameLen);

    }
}
