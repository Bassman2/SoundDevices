using SoundDevices.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SoundDevices.Engine
{
    public class MidiFile
    {
        
        public MidiFile()
        {
            this.Tracks = new();
            this.Description = string.Empty;
            this.Copyright = string.Empty;
        }

        public int NumOfTracks { get; private set; }
        public int MidiFileFormat { get; private set; }
        public int TicksPerQuarterNote { get; private set; }
        public List<MidiFileTrack> Tracks { get; }

        public string Description { get; set; }
        public string Copyright { get; set; }

        public void Load(string fileName)
        {
            using FileStream stream = File.Open(fileName, FileMode.Open);
            Load(stream);
        }

        public void Load(Stream stream)
        {
            using BinaryReader reader = new(stream);

            // read MIDI chunk header
            string chunkID = reader.ReadChunkID();
            if (chunkID != "MThd")
            {
                throw new MidiFileException("Incorrect chunk ID");
            }
            int chunkLength = reader.ReadBigEndianInt32();
            if (chunkLength != 6)
            {
                throw new MidiFileException("Incorrect chunk length");
            }
            this.MidiFileFormat = reader.ReadBigEndianInt16();
            if (this.MidiFileFormat < 0 || this.MidiFileFormat > 2)
            {
                throw new MidiFileException("MIDI file format not defined");
            }
            this.NumOfTracks = reader.ReadBigEndianInt16();
            this.TicksPerQuarterNote = reader.ReadBigEndianInt16();

            // read MIDI tracks
            for (int i = 0; i < this.NumOfTracks; i++)
            {
               this.Tracks.Add(new MidiFileTrack(this, reader));
            }
        }

        public void Save(string fileName)
        {
            using FileStream stream = File.Create(fileName);
            Save(stream);
        }

        public void Save(Stream stream)
        {
            using BinaryWriter writer = new(stream);
        }
    }
}
