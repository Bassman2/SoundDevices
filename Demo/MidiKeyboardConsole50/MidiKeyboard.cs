using SoundDevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MidiKeyboardConsole
{
    public class MidiKeyboard
    {
        public void Run(string[] args)
        {
            Console.WriteLine("Midi Keyboard Console");

            int deviceIndex = 0;
            int deviceChannel = 0;
            var devices = MidiOutDevice.GetDevices();

            foreach (var arg in args)
            {
                if (arg == "--help" || arg == "-h" || arg == "/?")
                {
                    Console.WriteLine("MidiKeyboardConsole [options]");
                    Console.WriteLine("Options:");
                    Console.WriteLine("  --help|-h|/?  show help");
                    Console.WriteLine($"  --device:<num>|-d:<num>|/d:<num> select device number [1-{devices.Count()}]");
                    Console.WriteLine("  --channel:<num>|-c:<num>|/c:<num> select device channel [1-16]");
                    return;
                }
                if (arg.StartsWith("--device:") || arg.StartsWith("-d:") || arg.StartsWith("/d:"))
                {
                    int.TryParse(arg.Substring(arg.IndexOf(':') + 1), out deviceIndex);
                }
                if (arg.StartsWith("--channel:") || arg.StartsWith("-c:") || arg.StartsWith("/c:"))
                {
                    int.TryParse(arg.Substring(arg.IndexOf(':') + 1), out deviceChannel);
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

            if (deviceChannel < 1)
            do
            {
                Console.Write("Select MIDI channel[1-16]:");
            } while (!int.TryParse(Console.ReadLine(), out deviceChannel) || deviceChannel < 1 || deviceChannel > 16);


            using MidiOutDevice device = devices.ElementAtOrDefault(deviceIndex - 1);
            if (device == null)
            {
                Console.WriteLine("MIDI device not available");
                return;
            }
            Console.WriteLine($"Output: {device.Name}");
            device.Open();

            Console.WriteLine(" |C|D| |F|G|A| |C|D|");
            Console.WriteLine(" |#|#| |#|#|#| |#|#|");
            Console.WriteLine(" |s|d| |g|h|j| |l|;|");
            Console.WriteLine("|C|D|E|F|G|A|B|C|D|E|");
            Console.WriteLine("|z|x|c|v|b|n|m|,|.|/|");
            Console.WriteLine("Esc to exit");

            ConsoleKeyInfo keyInfo;
            while ((keyInfo = Console.ReadKey()).Key != ConsoleKey.Escape)
            {
                MidiKeys key = keyInfo.Key switch
                {
                    ConsoleKey.Z => MidiKeys.C5,
                        ConsoleKey.S => MidiKeys.Db5,
                    ConsoleKey.X => MidiKeys.D5,
                        ConsoleKey.D => MidiKeys.Eb5,
                    ConsoleKey.C => MidiKeys.E5,
                    ConsoleKey.V => MidiKeys.F5,
                        ConsoleKey.G => MidiKeys.Gb5,
                    ConsoleKey.B => MidiKeys.G5,
                        ConsoleKey.H => MidiKeys.Ab5,
                    ConsoleKey.N => MidiKeys.A5,
                        ConsoleKey.J => MidiKeys.Bb5,
                    ConsoleKey.M => MidiKeys.B5,
                    ConsoleKey.OemComma => MidiKeys.C6,
                        ConsoleKey.L => MidiKeys.Db6,
                    ConsoleKey.OemPeriod => MidiKeys.D6,
                        ConsoleKey.Oem1 => MidiKeys.Eb6,
                    ConsoleKey.Oem2 => MidiKeys.E6,
                    _ => MidiKeys.C0
                };

                if (key != MidiKeys.C0)
                {
                    device.Send(new MidiMsg(MidiMessage.NoteOn, deviceChannel, key, 64));
                    Thread.Sleep(1000);
                    device.Send(new MidiMsg(MidiMessage.NoteOff, deviceChannel, key, 64));
                }
            }

            device.Close();
            Console.WriteLine("Bye!");


        }
    }
}
