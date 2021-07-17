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
            ref REFERENCE_TIME prtTime);

        void PackStructured(
            REFERENCE_TIME rt,
            uint dwChannelGroup,
            uint dwChannelMessage);

        void PackUnstructured(
            REFERENCE_TIME rt,
            uint dwChannelGroup,
            uint cb,
            ref BYTE lpb);

        void ResetReadPtr();

        void GetNextEvent(
            ref REFERENCE_TIME prt,
            ref DWORD pdwChannelGroup,
            ref DWORD pdwLength,
            ppDat a);

        void GetRawBufferPtr(
            ref REFERENCE_TIME prt,
            ref REFERENCE_TIME prt);

        void GetUsedBytes(
            ref DWORD pcb);

        void GetMaxBytes(
            ref DWORD pcb);

        void GetBufferFormat(
            ref Guid pGuidFormat);

        void SetStartTime(
            REFERENCE_TIME rt);

        void SetUsedBytes(
            uint cb);

    }
}