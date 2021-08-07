using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices
{
    public class MidiMsg
    {
        public MidiMsg(int value)
        {
            this.Message = (MidiMessage)((value & 0xF0) == 0x0F ? value & 0x0000FF : value & 0x0000F0); 
            this.Channel = value & 0x00000F;
            this.Key = (MidiKeys)((value & 0x00FF00) >> 8);
            this.Velocity = (value & 0xFF0000) >> 16;

        }

        public MidiMsg(MidiMessage msg)
        {
            this.Message = msg;
            this.Channel = 0;
            this.Key = 0;
            this.Velocity = 0;
        }

        public MidiMsg(MidiMessage msg, int channel, MidiKeys key, byte velocity)
        {
            this.Message = msg;
            this.Channel = channel;
            this.Key = key;
            this.Velocity = velocity;
        }

        public MidiMessage Message { get; }
        public int Channel { get; }
        public MidiKeys Key { get; }
        public int Velocity { get; }

        public int Value 
        { 
            get { return (int)this.Message | this.Channel | ((int)this.Key) << 8 | this.Velocity << 16; }        
        }

        public override string ToString()
        {
            return $"{this.Message}, {this.Channel}, {this.Key}, {this.Velocity}";
        }

        public static implicit operator int(MidiMsg m) => m.Value;
        public static implicit operator MidiMsg(int i) => new(i);
    }
}
