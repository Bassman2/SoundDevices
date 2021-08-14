namespace SoundDevices.IO
{
    public enum MidiMetaEvent : byte
    {
        SequenceNumber = 0x00, 
        TextEvent = 0x01, 
        CopyrightNotice = 0x02, 
        SequenceTrackName= 0x03, 
        InstrumentName = 0x04, 
        Lyric = 0x05, 
        Marker = 0x06, 
        CuePoint = 0x07, 
        ChannelPrefix = 0x20, 
        EndOfTrack = 0x2F, 
        Tempo = 0x51, // in microseconds per MIDI quarter-note
        SMPTEOffset = 0x54, 
        TimeSignature = 0x58, 
        KeySignature = 0x59, 
        SequencerSpecific = 0x7F, 
    }
}
