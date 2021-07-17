using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [ComImport]
    [Guid("d2ac2878-b39b-11d1-8704-00600893b1bd")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusicPortDownload
    {
        void GetBuffer(
            uint dwDLId,
            ppIDMDownloa d);

        void AllocateBuffer(
            uint dwSize,
            ppIDMDownloa d);

        void GetDLId(
            uint dwCount);

        void GetAppend(
            pIDMDownloa d);

        void Unload(
            level 1,
            IDirectMusicPor t,
            THIS_ REFIID,
            REFII D,
            ULON G,
            THI S);

        void PlayBuffer(
            ref DIRECTMUSICBUFFER pBuffer);

        void SetReadNotificationHandle(
            IntPtr hEvent);

        void Read(
            ref DIRECTMUSICBUFFER pBuffer);

        void DownloadInstrument(
            ppDownloadedInstrumen t,
            pNoteRange s,
            uint dwNumNoteRanges);

        void UnloadInstrument(
            ppCloc k);

        void GetRunningStats(
            ref DMUS_SYNTHSTATS pStats);

        void Compact();

        void GetCaps(
            ref DMUS_PORTCAPS pPortCaps);

        void DeviceIoControl(
            uint dwIoControlCode,
            ref VOID lpInBuffer,
            uint nInBufferSize,
            ref VOID lpOutBuffer,
            uint nOutBufferSize,
            ref DWORD lpBytesReturned,
            ref OVERLAPPED lpOverlapped);

        void SetNumChannelGroups(
            uint dwChannelGroups);

        void GetNumChannelGroups(
            ref DWORD pdwChannelGroups);

        void Activate(
            [MarshalAs(UnmanagedType.Bool)]
            bool fActive);

        void SetChannelPriority(
            uint dwChannelGroup,
            uint dwChannelGroup,
            ref DWORD pdwPriority,
            uint dwChannelGroup);

        void SetDirectSound(
            ref DIRECTSOUND pDirectSound,
            ref WAVEFORMATEX pWaveFormatEx,
            ref DWORD pdwBufferSize,
            ref WAVEFORMATEX pWaveFormatEx);

    }
}
