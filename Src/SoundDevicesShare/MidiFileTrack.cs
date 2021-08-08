using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SoundDevices
{
    public class MidiFileTrack
    {
        private readonly long ticksPerQuarterNote;
        private long midiTicks;


        public MidiFileTrack(Stream stream, int ticksPerQuarterNote)
        {
            this.Reader = new BinaryReader(stream);
            this.ticksPerQuarterNote = ticksPerQuarterNote;
            //this.midiTicks = 0; // ticksPerQuarterNote since start

            this.NextPlayTime = 0;
            this.Speed = 120; //BpM
            Next();
        }

        public BinaryReader Reader { get; }
        public long NextPlayTime { get; set; }
        public long Speed { get; set; }

        public long Next()
        {
            long delta = this.Reader.ReadMidiTime();
            this.NextPlayTime += delta * this.Speed * ticksPerQuarterNote / 60000;
            return this.NextPlayTime;
        }
    }
}
