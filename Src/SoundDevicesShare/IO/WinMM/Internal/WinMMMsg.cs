using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.WinMM.Internal
{
    public enum WinMMMsg : int
    {
        // waveform output 
        WOM_OPEN = 0x3BB,
        WOM_CLOSE = 0x3BC,
        WOM_DONE = 0x3BD,

        // waveform input 
        WIM_OPEN = 0x3BE,
        WIM_CLOSE = 0x3BF,
        WIM_DATA = 0x3C0,

        // MIDI input 

        /// <summary>
        /// The MIM_OPEN message is sent to a MIDI input callback function when a MIDI input device is opened.
        /// </summary>
        MIM_OPEN = 0x3C1,

        /// <summary>
        /// The MIM_CLOSE message is sent to a MIDI input callback function when a MIDI input device is closed.
        /// </summary>
        MIM_CLOSE = 0x3C2,

        /// <summary>
        /// The MIM_DATA message is sent to a MIDI input callback function when a MIDI message is received by a MIDI input device.
        /// </summary>
        MIM_DATA = 0x3C3,

        /// <summary>
        /// The MIM_LONGDATA message is sent to a MIDI input callback function when a system-exclusive buffer has been filled with data and is being returned to the application.
        /// </summary>
        MIM_LONGDATA = 0x3C4,

        /// <summary>
        /// The MIM_ERROR message is sent to a MIDI input callback function when an invalid MIDI message is received.
        /// </summary>
        MIM_ERROR = 0x3C5,

        /// <summary>
        /// The MIM_LONGERROR message is sent to a MIDI input callback function when an invalid or incomplete MIDI system-exclusive message is received.
        /// </summary>
        MIM_LONGERROR = 0x3C6,

        /// <summary>
        /// The MIM_MOREDATA message is sent to a MIDI input callback function when a MIDI message is received by a MIDI input device but the application is not processing MIM_DATA messages fast enough to keep up with the input device driver. 
        /// The callback function receives this message only when the application specifies MIDI_IO_STATUS in the call to the midiInOpen function.
        /// </summary>
        MIM_MOREDATA = 0x3CC,

        // MIDI output 

        /// <summary>
        /// The MOM_OPEN message is sent to a MIDI output callback function when a MIDI output device is opened.
        /// </summary>
        MOM_OPEN = 0x3C7,

        /// <summary>
        /// The WIM_CLOSE message is sent to the given waveform-audio input callback function when a waveform-audio input device is closed. The device handle is no longer valid after this message has been sent.
        /// </summary>
        MOM_CLOSE = 0x3C8,

        /// <summary>
        /// The MOM_DONE message is sent to a MIDI output callback function when the specified system-exclusive or stream buffer has been played and is being returned to the application.
        /// </summary>
        MOM_DONE = 0x3C9,

        MOM_POSITIONCB = 0x3CA,
    }
}
