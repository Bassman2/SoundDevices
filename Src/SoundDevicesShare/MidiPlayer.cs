using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace SoundDevices
{
    public class MidiPlayer
    {
        private MidiOutDevice midiOutDevice;

        private int midiFileFormat;
        private int numOfTracks;
        private int ticksPerQuarterNote;
        private List<MidiTrack> tracksStream;

        public MidiPlayer()
        { }

        public void Play(MidiOutDevice midiOutDevice, string fileName)
        {
            using FileStream midiStream = File.Open(fileName, FileMode.Open);
            Play(midiOutDevice, midiStream);
        }

        public void Play(MidiOutDevice midiOutDevice, Stream midiStream)
        {
            using BinaryReader reader = new BinaryReader(midiStream);

            ReadHeaderChunk(reader);

            this.tracksStream = new(); 
            for (int i = 0; i < this.numOfTracks; i++)
            {
                Stream trkStream = ReadTrackChunk(reader);
                MidiTrack midiTrack = new MidiTrack(trkStream);
                this.tracksStream.Add(midiTrack);
            }

            PlayTracks();
        }

        private void ReadHeaderChunk(BinaryReader reader)
        {
            string chunkID = reader.ReadChunkID();
            if (chunkID != "MThd")
            {
                throw new Exception("Not a midi file");
            }
            int chunkLength = reader.ReadBigEndianInt32();
            if (chunkLength != 6)
            {
                throw new Exception("Incorrect chunk length");
            }
            this.midiFileFormat = reader.ReadBigEndianInt16();
            if (this.midiFileFormat < 0 || this.midiFileFormat > 2)
            {
                throw new Exception("MIDI file format not defined");
            }
            this.numOfTracks = reader.ReadBigEndianInt16();
            if (this.numOfTracks != 1)
            {
                throw new Exception("Only one track supported");
            }
            this.ticksPerQuarterNote = reader.ReadBigEndianInt16();
        }

        private Stream ReadTrackChunk(BinaryReader reader)
        {
            string chunkID = reader.ReadChunkID();
            if (chunkID != "MTrk")
            {
                throw new Exception("Not a midi track");
            }
            int chunkLength = reader.ReadBigEndianInt32();
            if (chunkLength < 0)
            {
                throw new Exception("Incorrect chunk length");
            }

            MemoryStream trackStream = new MemoryStream(chunkLength);
            reader.CopyToStream(trackStream, chunkLength);
            trackStream.Seek(0, SeekOrigin.Begin);
            return trackStream;
        }

        private void PlayTracks()
        {
            long startTime = Environment.TickCount64;
            long playTime;
            bool running = true;
            while (running)
            {
                playTime = Environment.TickCount64 - startTime;

            }
        }

        private class MidiTrack
        {
            private readonly long ticksPerQuarterNote;
            private long midiTicks;
            

            public MidiTrack(Stream stream, int ticksPerQuarterNote)
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
}
