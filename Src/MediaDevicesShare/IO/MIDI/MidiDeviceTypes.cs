using System;
using System.Collections.Generic;
using System.Text;

namespace MediaDevices.IO.MIDI
{
    [Flags]
    public enum MidiDeviceTypes
    {
        All = 0xffff,
        WinMM = 0x0001
    }
}
