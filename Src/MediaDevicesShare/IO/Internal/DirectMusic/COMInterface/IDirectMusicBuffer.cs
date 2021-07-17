using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [ComImport]
    [Guid("d2ac2878-b39b-11d1-8704-00600893b1bd")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusicBuffer
    {
        void Flush();

        void TotalTime(
            LPREFERENCE_TIME prtTime);

        void PackStructured(
            REFERENCE_TIME rt,
            uint dwChannelGroup,
            uint dwChannelMessage);

        void PackUnstructured(
            REFERENCE_TIME rt,
            uint dwChannelGroup,
            uint cb,
            LPBYTE lpb);

        void ResetReadPtr();

        void GetNextEvent(
            LPREFERENCE_TIME prt,
            LPDWORD pdwChannelGroup,
            LPDWORD pdwLength,
            ppDat a);

        void GetRawBufferPtr(
            LPREFERENCE_TIME prt,
            LPREFERENCE_TIME prt);

        void GetUsedBytes(
            LPDWORD pcb);

        void GetMaxBytes(
            LPDWORD pcb);

        void GetBufferFormat(
            ref Guid pGuidFormat);

        void SetStartTime(
            REFERENCE_TIME rt);

        void SetUsedBytes(
            uint cb);

    }
}
