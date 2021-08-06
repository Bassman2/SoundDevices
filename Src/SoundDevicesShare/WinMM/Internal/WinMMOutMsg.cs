using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.WinMM.Internal
{
    internal enum WinMMOutMsg
    {
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
        
        //MOM_POSITIONCB
    }
}
