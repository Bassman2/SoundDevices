using System;
using System.Runtime.InteropServices;

namespace MacPuncture50
{
    class Program
    {
        public const string CoreMidiLibrary = "/System/Library/Frameworks/CoreMIDI.framework/CoreMIDI";
        public const string SystemLibrary = "/usr/lib/libSystem.dylib";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var midiLibrary = dlopen(CoreMidiLibrary, 0);


            var indirect = dlsym(midiLibrary, "kMIDIPropertyDisplayName");
            if (indirect == IntPtr.Zero)
                return IntPtr.Zero;
            IntPtr displayName Marshal.ReadIntPtr(indirect);





            var indirect = dlsym(handle, symbol);
            if (indirect == IntPtr.Zero)
                return IntPtr.Zero;
            return Marshal.ReadIntPtr(indirect);
            kMIDIPropertyDisplayName = Dlfcn.GetIntPtr(midiLibrary, "kMIDIPropertyDisplayName");
            dlsym(IntPtr handle, string symbol);

            int devNum = MIDIGetNumberOfDevices();
            Console.WriteLine($"MIDIGetNumberOfDevices: {devNum}");

            for (int i = 0; i < devNum; i++)
            {
                IntPtr ptr = MIDIGetDevice(i);
            }

            int extDevNum = MIDIGetNumberOfExternalDevices();
            Console.WriteLine($"MIDIGetNumberOfExternalDevices: {extDevNum}");

            
            int desNum = MIDIGetNumberOfDestinations();
            Console.WriteLine($"MIDIGetNumberOfDestinations: {desNum}");

            int srcNum = MIDIGetNumberOfSources();
            Console.WriteLine($"MIDIGetNumberOfSources: {srcNum}");
        }

        public static MidiDevice GetDevice(int deviceIndex)
        {
            var h = MIDIGetDevice(deviceIndex);
            if (h == IntPtr.Zero)
                return null;
            return new MidiDevice(h);
        }

        public static MidiDevice GetExternalDevice(int deviceIndex)
        {
            var h = MIDIGetExternalDevice(deviceIndex);
            if (h == IntPtr.Zero)
                return null;
            return new MidiDevice(h);
        }

        public static string MIDIObjectGetStringProperty(IntPtr handle)
        {
            IntPtr val;
            int code;

            //kMIDIPropertyDisplayName = Dlfcn.GetIntPtr(midiLibrary, "kMIDIPropertyDisplayName");

            code = MIDIObjectGetStringProperty(handle, property, out val);
            if (code == 0)
            {
                string str = Marshal.PtrToStringAnsi(val);
                Marshal.Release(val);
                
                return str;
            }
            return null;
        }

        
        [DllImport(SystemLibrary)]
        private static extern int dlclose(IntPtr handle);

        [DllImport(SystemLibrary)]
        private static extern IntPtr dlopen(string path, int mode);

        [DllImport(SystemLibrary)]
        private static extern IntPtr dlsym(IntPtr handle, string symbol);

        [DllImport(SystemLibrary, EntryPoint = "dlerror")]
        private static extern IntPtr dlerror_();


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

        [DllImport(CoreMidiLibrary)]
        private extern static IntPtr MIDIGetExternalDevice(int item);

        [DllImport(CoreMidiLibrary)]
        private extern static IntPtr MIDIGetDevice(int item);

        [DllImport(CoreMidiLibrary)]
        private extern static int MIDIObjectGetStringProperty(IntPtr obj, IntPtr str, out IntPtr data);

    }
}
