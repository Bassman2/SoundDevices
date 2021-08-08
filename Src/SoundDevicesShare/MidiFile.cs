using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SoundDevices
{
    public class MidiFile
    {
        
        public MidiFile()
        {
            this.Tracks = new();
        }

        public int NumOfTracks { get; private set; }
        public int MidiFileFormat { get; private set; }
        public int TicksPerQuarterNote { get; private set; }
        public List<MidiFileTrack> Tracks { get; }

        public void Load(string fileName)
        {
            using FileStream midiStream = File.Open(fileName, FileMode.Open);
            Load(midiStream);
        }

        public void Load(Stream midiStream)
        {
            using BinaryReader reader = new BinaryReader(midiStream);

            ReadHeaderChunk(reader);
                        
            for (int i = 0; i < this.NumOfTracks; i++)
            {
                Stream trkStream = ReadTrackChunk(reader);
                MidiFileTrack midiTrack = new MidiFileTrack(trkStream, this.TicksPerQuarterNote);
                this.Tracks.Add(midiTrack);
            }
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
            this.MidiFileFormat = reader.ReadBigEndianInt16();
            if (this.MidiFileFormat < 0 || this.MidiFileFormat > 2)
            {
                throw new Exception("MIDI file format not defined");
            }
            this.NumOfTracks = reader.ReadBigEndianInt16();
            this.TicksPerQuarterNote = reader.ReadBigEndianInt16();
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

    }
}
