using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace SoundDevices.IO
{
    public class MidiFileTrack
    {
        private MidiFile midiFile;
        private MemoryStream trackStream;

        public int SequenceNumber { get; set; }
        public string Name { get; set; }
        public string Instrument { get; set; }

        internal MidiFileTrack(MidiFile midiFile)
        {
            this.midiFile = midiFile;
            this.trackStream = new MemoryStream();

            this.SequenceNumber = 0;
            this.Name = string.Empty;
            this.Instrument = string.Empty;
        }
        
        internal MidiFileTrack(MidiFile midiFile, BinaryReader reader) : this(midiFile)
        {
            // load MIDI track chunk 
            string chunkID = reader.ReadChunkID();
            if (chunkID != "MTrk")
            {
                throw new MidiFileException("Not a midi track");
            }
            int chunkLength = reader.ReadBigEndianInt32();
            if (chunkLength < 0)
            {
                throw new MidiFileException("Incorrect chunk length");
            }

            // copy track to MemoryStream
            this.trackStream.Capacity = chunkLength;
            reader.CopyToStream(this.trackStream, chunkLength);
            this.trackStream.Seek(0, SeekOrigin.Begin);
            ScanTrack();
            this.trackStream.Seek(0, SeekOrigin.Begin);
        }

        private void ScanTrack()
        {
            using BinaryReader reader = new(this.trackStream, Encoding.ASCII, true);

            byte key, vel, ctr, val, prg, prs, sng;
            short pit, pos;
            byte len, nn, dd, cc, bb;
            int tempo;

            while (true)
            {
                long delta = reader.ReadMidiTime();
                byte b = reader.ReadByte();
                MidiMessage msg = (MidiMessage)b;
                MidiMessage cmd = (MidiMessage)(b & ((byte)0xf0));
                byte chn = (byte)(b & ((byte)0x0f));
                

                MidiMetaEvent metaEvent;

                switch (cmd)
                {
                case MidiMessage.NoteOff:
                    key = reader.ReadByte(); // key
                    vel = reader.ReadByte(); // velocity
                    break;
                case MidiMessage.NoteOn:
                    key = reader.ReadByte(); // key
                    vel = reader.ReadByte(); // velocity
                    break;
                case MidiMessage.Aftertouch:
                    key = reader.ReadByte(); // key
                    vel = reader.ReadByte(); // velocity
                    break;
                case MidiMessage.ControlChange:
                    ctr = reader.ReadByte(); // controller
                    val = reader.ReadByte(); // value
                    break;
                case MidiMessage.ProgramChange:
                    prg = reader.ReadByte(); // program no#
                    break;
                case MidiMessage.ChannelPressure:
                    prs = reader.ReadByte(); // pressure
                    break;
                case MidiMessage.PitchWheelChange:
                    pit = reader.ReadMidiInt16(); // pitch wheel value
                    break;
                case MidiMessage.SystemExclusive:
                    switch (msg)
                    {
                    case MidiMessage.SystemExclusive:
                        reader.ReadMidiSysEx();
                        break;
                    case MidiMessage.SongPositionPointer:
                        pos = reader.ReadMidiInt16(); // song position
                        break;
                    case MidiMessage.SongSelect:
                        sng = reader.ReadByte();
                        break;
                    case MidiMessage.TuneRequest:
                        break;
                    //case MidiMessage.EndOfExclusive:
                    //    break;
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
                        metaEvent = (MidiMetaEvent)reader.ReadByte();
                        switch (metaEvent)
                        {
                        case MidiMetaEvent.SequenceNumber:
                            this.SequenceNumber = reader.ReadByte();
                            break;
                        case MidiMetaEvent.TextEvent:
                            this.midiFile.Description += reader.ReadMidiText();
                            break;
                        case MidiMetaEvent.CopyrightNotice:
                            this.midiFile.Copyright += reader.ReadMidiText();
                            break;
                        case MidiMetaEvent.SequenceTrackName:
                            this.Name += reader.ReadMidiText();
                            break;
                        case MidiMetaEvent.InstrumentName:
                            this.Instrument += reader.ReadMidiText();
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
                            len = reader.ReadByte(); // = 00
                            return;
                        case MidiMetaEvent.Tempo:
                            len = reader.ReadByte(); // = 03
                            tempo = reader.ReadBigEndianInt24();
                            break;
                        case MidiMetaEvent.SMPTEOffset:
                            break;
                        case MidiMetaEvent.TimeSignature:
                            len = reader.ReadByte(); // = 04
                            nn = reader.ReadByte();
                            dd = reader.ReadByte();
                            cc = reader.ReadByte();
                            bb = reader.ReadByte();
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
        }
    }
}
