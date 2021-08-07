namespace SoundDevices
{
    public enum MidiMessage : byte
    {
        //
        // Channel Voice Messages (only high nibble, low nibble is channel)
        //

        NoteOff = 0x80,
        NoteOn = 0x90,
        Aftertouch = 0xA0,
        ControlChange = 0xB0,         //  Local Control is Off
        ProgramChange = 0xC0,
        ChannelPressure = 0xD0,
        PitchWheelChange = 0xE0,

        //
        // Channel Mode Messages 
        //

        ChannelMode = 0xB0,     //  Local Control is On

        //
        // System Common Messages (complete byte)
        //

        SystemExclusive = 0xF0,
        // 0xF1 undefined
        SongPositionPointer = 0xF2,
        SongSelect = 0xF3,
        // 0xF4 undefined
        // 0xF5 undefined
        TuneRequest = 0xF6,
        EndOfExclusive = 0xF7,

        //
        // System Real-Time Messages
        //

        TimingClock = 0xF8,
        // 0xF9 undefined
        Start = 0xFA,
        Continue = 0xFB,
        Stop = 0xFC,
        // 0xFD undefined
        ActiveSensing = 0xFE,
        Reset = 0xFF,           // for use on MIDI devices
        MetaEvent = 0xFF        // for use in MIDI files
    }
}
