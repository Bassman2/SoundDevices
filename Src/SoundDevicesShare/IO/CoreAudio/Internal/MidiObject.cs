using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SoundDevices.IO.CoreMidi.Internal
{
    public abstract class MidiObject
    {
        public const string CoreMidiLibrary = "/System/Library/Frameworks/CoreMIDI.framework/CoreMIDI";


        [DllImport(CoreMidiLibrary)]
        private extern static int MIDIObjectGetStringProperty(IntPtr obj, IntPtr str, out IntPtr data);
    }
}
