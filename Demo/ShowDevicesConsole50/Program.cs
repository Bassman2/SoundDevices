using SoundDevices;
using System;

namespace ShowDevicesConsole50
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("SoundDevices device list");

            Console.WriteLine("  Midi In devices");
            foreach (var midiInDevice in MidiInDevice.GetDevices())
            {
                Console.WriteLine($"    {midiInDevice.Name}, {midiInDevice.Version}, {midiInDevice.DeviceType}, {midiInDevice.Description}");
            }
            Console.WriteLine();

            Console.WriteLine("  Midi Out devices");
            foreach (var midiOutDevice in MidiOutDevice.GetDevices())
            {
                Console.WriteLine($"    {midiOutDevice.Name}, {midiOutDevice.Version}, {midiOutDevice.DeviceType}, {midiOutDevice.Description}");
            }
            Console.WriteLine();

            Console.WriteLine("  Wave In devices");
            foreach (var waveInDevice in WaveInDevice.GetDevices())
            {
                Console.WriteLine($"    {waveInDevice.Name}, {waveInDevice.Version}, {waveInDevice.DeviceType}, {waveInDevice.Description}");

            }
            Console.WriteLine();

            Console.WriteLine("  Wave Out devices");
            foreach (var waveOutDevice in WaveOutDevice.GetDevices())
            {
                Console.WriteLine($"    {waveOutDevice.Name}, {waveOutDevice.Version}, {waveOutDevice.DeviceType}, {waveOutDevice.Description}");

            }
        }
    }
}
