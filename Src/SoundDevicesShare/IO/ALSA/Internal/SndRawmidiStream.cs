using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.ALSA.Internal
{
    // snd_rawmidi_stream
    
    public enum SndRawmidiStream
    {
        /** Output stream */
        Output = 0,             // SND_RAWMIDI_STREAM_OUTPUT
        /** Input stream */
        Input,                  // SND_RAWMIDI_STREAM_INPUT
        Last = Input
    }
}
