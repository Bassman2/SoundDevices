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
        private List<MidiTrack> tracks;

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

            this.tracks = new(); 
            for (int i = 0; i < this.numOfTracks; i++)
            {
                Stream trkStream = ReadTrackChunk(reader);
                MidiTrack midiTrack = new MidiTrack(trkStream, this.ticksPerQuarterNote);
                this.tracks.Add(midiTrack);
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
                foreach (var track in this.tracks)
                {
                    PlayEvent(track);
                    track.Next();
                }
            }
        }

        private void PlayEvent(MidiTrack track)
        {
            byte b = track.Reader.ReadByte();
            MidiMessage msg = (MidiMessage)b;
            MidiMessage cmd = (MidiMessage)(b & ((byte)0xf0));
            byte chn = (byte)(b & ((byte)0x0f));
            byte key, vel, ctr, val, prg, prs, sng;
            short pit, pos;
            MidiMetaEvent metaEvent;


            switch (cmd)
            {
            case MidiMessage.NoteOff: 
                key = track.Reader.ReadByte(); // key
                vel = track.Reader.ReadByte(); // velocity
                break;
            case MidiMessage.NoteOn: 
                key = track.Reader.ReadByte(); // key
                vel = track.Reader.ReadByte(); // velocity
                break;
            case MidiMessage.Aftertouch: 
                key = track.Reader.ReadByte(); // key
                vel = track.Reader.ReadByte(); // velocity
                break;
            case MidiMessage.ControlChange: 
                ctr = track.Reader.ReadByte(); // controller
                val = track.Reader.ReadByte(); // value
                break;
            case MidiMessage.ProgramChange: 
                prg = track.Reader.ReadByte(); // program no#
                break;
            case MidiMessage.ChannelPressure: 
                prs = track.Reader.ReadByte(); // pressure
                break;
            case MidiMessage.PitchWheelChange: 
                pit = track.Reader.ReadMidiInt16(); // pitch wheel value
                break;
            case MidiMessage.SystemExclusive:
                switch (msg)
                {
                case MidiMessage.SystemExclusive:
                    break;
                case MidiMessage.SongPositionPointer:
                    pos = track.Reader.ReadMidiInt16(); // song position
                    break;
                case MidiMessage.SongSelect:
                    sng = track.Reader.ReadByte(); 
                    break;
                case MidiMessage.TuneRequest:
                    break;
                case MidiMessage.EndOfExclusive:
                    break;
                case MidiMessage.TimingClock:
                    break;
                case MidiMessage.Start:
                    break;
                case MidiMessage.Continue:
                    break;
                case MidiMessage.Stop:
                    break;
                case MidiMessage.ActiveSensing:
                    break;
                case MidiMessage.MetaEvent:
                    metaEvent = (MidiMetaEvent)track.Reader.ReadByte();
                    switch (metaEvent)
                    {
                    case MidiMetaEvent.SequenceNumber:
                        byte seqNum = track.Reader.ReadByte();
                        break;
                    case MidiMetaEvent.TextEvent:
                        break;
                    case MidiMetaEvent.CopyrightNotice:
                        break;
                    case MidiMetaEvent.SequenceTrackName:
                        break;
                    case MidiMetaEvent.InstrumentName: 
                        break;
                    case MidiMetaEvent.Lyric:
                        break;
                    case MidiMetaEvent.Marker:
                        break;
                    case MidiMetaEvent.CuePoint:
                        break;
                    case MidiMetaEvent.ChannelPrefix:
                        break;
                    case MidiMetaEvent.EndOfTrack:
                        break;
                    case MidiMetaEvent.Tempo:
                        break;
                    case MidiMetaEvent.SMPTEOffset:
                        break;
                    case MidiMetaEvent.TimeSignature:
                        break;
                    case MidiMetaEvent.KeySignature:
                        break;
                    case MidiMetaEvent.SequencerSpecific:
                        break;
                    default:
                        throw new Exception($"Unknown MIDI meta event {metaEvent}");
                    }
                    break;
                default:
                    throw new Exception($"Unknown MIDI message {msg}");
                }
                break;
            default:
                break;
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
