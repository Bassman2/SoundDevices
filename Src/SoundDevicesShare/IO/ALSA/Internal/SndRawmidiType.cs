using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.ALSA.Internal
{
    
    //snd_rawmidi_type 
    public enum SndRawmidiType
    {
        /** Kernel level RawMidi */
        SND_RAWMIDI_TYPE_HW,
        /** Shared memory client RawMidi (not yet implemented) */
        SND_RAWMIDI_TYPE_SHM,
        /** INET client RawMidi (not yet implemented) */
        SND_RAWMIDI_TYPE_INET,
        /** Virtual (sequencer) RawMidi */
        SND_RAWMIDI_TYPE_VIRTUAL
    }
}
