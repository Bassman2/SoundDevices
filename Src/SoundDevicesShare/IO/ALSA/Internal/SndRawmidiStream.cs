using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.ALSA.Internal
{
    // snd_rawmidi_stream
    
    public enum SndRawmidiStream
    {
        /** Output stream */
        Output = 0,
        /** Input stream */
        Input,
        Last = Input
    }
}
