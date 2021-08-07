using SoundDevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiViewerConsole50
{
    public class MidiViewer
    {
        public void Run(string[] args)
        {
            Console.WriteLine("Midi Viewer Console");

            int deviceIndex = 0;
            var devices = MidiInDevice.GetDevices();

            foreach (var arg in args)
            {
                if (arg == "--help" || arg == "-h" || arg == "/?")
                {
                    Console.WriteLine("MidiViewerConsole [options]");
                    Console.WriteLine("Options:");
                    Console.WriteLine("  --help|-h|/?  show help");
                    Console.WriteLine($"  --device:<num>|-d:<num>|/d:<num> select device number [1-{devices.Count()}]");
                    return;
                }
                if (arg.StartsWith("--device:") || arg.StartsWith("-d:") || arg.StartsWith("/d:"))
                {
                    int.TryParse(arg.Substring(arg.IndexOf(':') + 1), out deviceIndex);
                }
            }

            if (deviceIndex < 1)
            {
                Console.WriteLine("MIDI devices");
                int index = 1;
                foreach (var dev in devices)
                {
                    Console.WriteLine($"{index++}: {dev.Name} {dev.DeviceType}");
                }

                do
                {
                    Console.Write($"Select MIDI device[1-{devices.Count()}]:");
                } while (!int.TryParse(Console.ReadLine(), out deviceIndex) || deviceIndex < 1 || deviceIndex > devices.Count());
            }

            using MidiInDevice device = devices.ElementAtOrDefault(deviceIndex);
            if (device == null)
            {
                Console.WriteLine("MIDI device not available");
                return;
            }
            Console.WriteLine($"Output: {device.Name}");
            Console.WriteLine("Esc to exit");
            device.MidiMsgReceived += OnMidiMsgReceived;
            device.Open();
            device.Start();

            while (Console.ReadKey().Key != ConsoleKey.Escape);

            device.Stop();
            device.Close();
        }

        private void OnMidiMsgReceived(object sender, MidiMsgEventArgs e)
        {
            MidiMsg msg = e.MidiMsg;
            Console.WriteLine(msg.ToString());
        }
    }
}
