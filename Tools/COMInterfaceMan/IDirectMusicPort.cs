using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [ComImport]
    [Guid("08f2d8c9-37c2-11d2-b9f9-0000f875ac12")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusicPort
    {
        //void PlayBuffer(LPDIRECTMUSICBUFFER pBuffer);
        //void SetReadNotificationHandle(IntPtr hEvent);
        //void Read(LPDIRECTMUSICBUFFER pBuffer);
        //void DownloadInstrument(IDirectMusicInstrument* pInstrument, IDirectMusicDownloadedInstrument** ppDownloadedInstrument, DMUS_NOTERANGE* pNoteRanges, DWORD dwNumNoteRanges);
        //void UnloadInstrument(IDirectMusicDownloadedInstrument* pDownloadedInstrument);
        //void GetLatencyClock(IReferenceClock** ppClock);
        //void GetRunningStats(LPDMUS_SYNTHSTATS pStats);
        //void Compact();
        //void GetCaps(LPDMUS_PORTCAPS pPortCaps);
        //void DeviceIoControl(DWORD dwIoControlCode, LPVOID lpInBuffer, DWORD nInBufferSize, LPVOID lpOutBuffer, DWORD nOutBufferSize, LPDWORD lpBytesReturned, LPOVERLAPPED lpOverlapped);
        //void SetNumChannelGroups(DWORD dwChannelGroups);
        //void GetNumChannelGroups(LPDWORD pdwChannelGroups);
        //void Activate(bool fActive);
        //void SetChannelPriority(DWORD dwChannelGroup, DWORD dwChannel, DWORD dwPriority);
        //void GetChannelPriority(DWORD dwChannelGroup, DWORD dwChannel, LPDWORD pdwPriority);
        //void SetDirectSound(LPDIRECTSOUND pDirectSound, LPDIRECTSOUNDBUFFER pDirectSoundBuffer);
        //void GetFormat(LPWAVEFORMATEX pWaveFormatEx, LPDWORD pdwWaveFormatExSize, LPDWORD pdwBufferSize);
    }
}
