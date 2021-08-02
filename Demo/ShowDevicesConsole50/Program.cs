using MediaDevices.IO.MIDI;
using MediaDevices.IO.Wave;
using System;

namespace ShowDevicesConsole50
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SoundDevices device list");

            Console.WriteLine("  Midi In devices");
            foreach (var midiInDevice in MidiInDevice.GetDevices(MidiDeviceTypes.All))
            {
                Console.WriteLine($"    {midiInDevice.Name}, {midiInDevice.Version}, {midiInDevice.InterfaceType}, {midiInDevice.Description}");
            }
            Console.WriteLine();

            Console.WriteLine("  Midi Out devices");
            foreach (var midiOutDevice in MidiOutDevice.GetDevices(MidiDeviceTypes.All))
            {
                Console.WriteLine($"    {midiOutDevice.Name}, {midiOutDevice.Version}, {midiOutDevice.InterfaceType}, {midiOutDevice.Description}");
            }
            Console.WriteLine();

            Console.WriteLine("  Wave In devices");
            foreach (var waveInDevice in WaveInDevice.GetDevices(WaveDeviceTypes.All))
            {
                Console.WriteLine($"    {waveInDevice.Name}, {waveInDevice.Version}, {waveInDevice.InterfaceType}, {waveInDevice.Description}");

            }
            Console.WriteLine();

            Console.WriteLine("  Wave Out devices");
            foreach (var waveOutDevice in WaveOutDevice.GetDevices(WaveDeviceTypes.All))
            {
                Console.WriteLine($"    {waveOutDevice.Name}, {waveOutDevice.Version}, {waveOutDevice.InterfaceType}, {waveOutDevice.Description}");

            }
        }
    }
}
