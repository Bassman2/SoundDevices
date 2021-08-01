using System;
using System.Runtime.InteropServices;

namespace MacPuncture50
{
    class Program
    {
        public const string CoreMidiLibrary = "/System/Library/Frameworks/CoreMIDI.framework/CoreMIDI";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            int devNum = MIDIGetNumberOfDevices();
            Console.WriteLine($"MIDIGetNumberOfDevices: {devNum}");

            int extDevNum = MIDIGetNumberOfExternalDevices();
            Console.WriteLine($"MIDIGetNumberOfExternalDevices: {extDevNum}");

            
            int desNum = MIDIGetNumberOfDestinations();
            Console.WriteLine($"MIDIGetNumberOfDestinations: {desNum}");

            int srcNum = MIDIGetNumberOfSources();
            Console.WriteLine($"MIDIGetNumberOfSources: {srcNum}");
        }

        [DllImport(CoreMidiLibrary)]
        private extern static void MIDIRestart();

        [DllImport(CoreMidiLibrary)]
        private extern static int MIDIGetNumberOfDevices();

        [DllImport(CoreMidiLibrary)]
        private extern static int MIDIGetNumberOfExternalDevices();


        [DllImport(CoreMidiLibrary)]
        private extern static int MIDIGetNumberOfDestinations();

        [DllImport(CoreMidiLibrary)]
        private extern static int MIDIGetNumberOfSources();

        


    }
}
