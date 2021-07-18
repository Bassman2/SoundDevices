using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [ComImport]
    [Guid("08f2d8c9-37c2-11d2-b9f9-0000f875ac12")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusicPort
    {
        void PlayBuffer(
            ref IDirectMusicBuffer pBuffer);

        void SetReadNotificationHandle(
            IntPtr hEvent);

        void Read(
            ref IDirectMusicBuffer pBuffer);

        void DownloadInstrument(
            IDirectMusicInstrument pInstrument,
            IDirectMusicDownloadedInstrument ppDownloadedInstrument,
            DMUS_NOTERANGE pNoteRanges,
            uint dwNumNoteRanges);

        void UnloadInstrument(
            IDirectMusicDownloadedInstrument pDownloadedInstrument);

        void GetLatencyClock(
            IReferenceClock ppClock);

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
