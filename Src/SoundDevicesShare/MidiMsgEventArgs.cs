using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices
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
