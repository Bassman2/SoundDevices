using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.DirectX.Internal
{
    internal interface IDirectMusicPort
    {
        //  IDirectMusicPort 
        
        void PlayBuffer(ref IDirectMusicBuffer pBuffer);
        void SetReadNotificationHandle(IntPtr hEvent);
        void Read(ref IDirectMusicBuffer pBuffer);
        void DownloadInstrument(ref IDirectMusicInstrument pInstrument, out IDirectMusicDownloadedInstrument ppDownloadedInstrument, ref DMUS_NOTERANGE pNoteRanges, int dwNumNoteRanges);
        void UnloadInstrument(ref IDirectMusicDownloadedInstrument pDownloadedInstrument);
        void GetLatencyClock(out IReferenceClock ppClock);
        void GetRunningStats(ref DMUS_SYNTHSTATS pStats);
        void Compact();
        void GetCaps(ref DMUS_PORTCAPS pPortCaps);
        void DeviceIoControl(int dwIoControlCode, IntPtr lpInBuffer, int nInBufferSize, IntPtr lpOutBuffer, int nOutBufferSize, out int lpBytesReturned, ref OVERLAPPED lpOverlapped);
        void SetNumChannelGroups(int dwChannelGroups);
        void GetNumChannelGroups(out int pdwChannelGroups);
        void Activate(int fActive);
        void SetChannelPriority(int dwChannelGroup, int dwChannel, int dwPriority);
        void GetChannelPriority(int dwChannelGroup, int dwChannel, out int pdwPriority);
        void SetDirectSound(ref IDirectSound pDirectSound, ref IDirectSoundBuffer pDirectSoundBuffer);
        void GetFormat(ref WaveFormatEx pWaveFormatEx, out int pdwWaveFormatExSize, out int pdwBufferSize);
    }
}
