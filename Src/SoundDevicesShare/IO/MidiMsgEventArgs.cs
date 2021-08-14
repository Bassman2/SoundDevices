using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO
{
    public class MidiMsgEventArgs : EventArgs
    {
        public MidiMsgEventArgs(int midiMsg)
        {
            this.MidiMsg = midiMsg;
        }

        public int MidiMsg { get; }
    }
}
