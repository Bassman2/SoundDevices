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
            LPDIRECTMUSICBUFFER pBuffer);

        void SetReadNotificationHandle(
            IntPtr hEvent);

        void Read(
            LPDIRECTMUSICBUFFER pBuffer);

        void DownloadInstrument(
            ppDownloadedInstrumen t,
            pNoteRange s,
            uint dwNumNoteRanges);

        void UnloadInstrument(
            ppCloc k);

        void GetRunningStats(
            LPDMUS_SYNTHSTATS pStats);

        void Compact();

        void GetCaps(
            LPDMUS_PORTCAPS pPortCaps);

        void DeviceIoControl(
            uint dwIoControlCode,
            LPVOID lpInBuffer,
            uint nInBufferSize,
            LPVOID lpOutBuffer,
            uint nOutBufferSize,
            LPDWORD lpBytesReturned,
            LPOVERLAPPED lpOverlapped);

        void SetNumChannelGroups(
            uint dwChannelGroups);

        void GetNumChannelGroups(
            LPDWORD pdwChannelGroups);

        void Activate(
            [MarshalAs(UnmanagedType.Bool)]
            bool fActive);

        void SetChannelPriority(
            uint dwChannelGroup,
            uint dwChannelGroup,
            LPDWORD pdwPriority,
            uint dwChannelGroup);

        void SetDirectSound(
            LPDIRECTSOUND pDirectSound,
            LPWAVEFORMATEX pWaveFormatEx,
            LPDWORD pdwBufferSize,
            LPWAVEFORMATEX pWaveFormatEx);

    }
}
