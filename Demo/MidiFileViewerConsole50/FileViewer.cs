using SoundDevices.Engine;
using SoundDevices.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiFileViewerConsole50
{
    public class FileViewer
    {
        public void Run(string[] args)
        {
            Console.WriteLine("Midi File Viewer Console");

            string fileName = string.Empty;
            
            foreach (var arg in args)
            {
                if (arg == "--help" || arg == "-h" || arg == "/?")
                {
                    Console.WriteLine("MidiKeyboardConsole [options]");
                    Console.WriteLine("Options:");
                    Console.WriteLine("  --help|-h|/?  show help");
                    Console.WriteLine("  <filename> MIDI file name");
                    return;
                }
                fileName = arg;
            }

            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
            {
                Console.WriteLine("File not found");
                return;
            }

            MidiFile midiFile = new();
            midiFile.Load(fileName);

            Console.WriteLine($"FileFormat (1-3): {midiFile.MidiFileFormat + 1}");

            Console.WriteLine($"NumOfTracks: {midiFile.NumOfTracks}");
            Console.WriteLine($"TicksPerQuarterNote: {midiFile.TicksPerQuarterNote}");

            Console.WriteLine($"Description: {midiFile.Description}");
            Console.WriteLine($"Copyright: {midiFile.Copyright}");

            int index = 1;
            foreach(var track in midiFile.Tracks)
            {
                Console.WriteLine($"Track {index}");
                Console.WriteLine($"  Number {track.SequenceNumber}");
                Console.WriteLine($"  Name: {track.Name}");
                Console.WriteLine($"  Instrument: {track.Instrument}");
                index++;
            }
        }
    }
}
