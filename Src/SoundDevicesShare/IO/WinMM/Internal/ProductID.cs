using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.WinMM.Internal
{
    internal enum ProductID
    {
        MM_MIDI_MAPPER = 1,       /*  Midi Mapper  */
        MM_WAVE_MAPPER = 2,       /*  Wave Mapper  */
        MM_SNDBLST_MIDIOUT = 3,       /*  Sound Blaster MIDI output port  */
        MM_SNDBLST_MIDIIN = 4,       /*  Sound Blaster MIDI input port  */
        MM_SNDBLST_SYNTH = 5,       /*  Sound Blaster internal synth  */
        MM_SNDBLST_WAVEOUT = 6,       /*  Sound Blaster waveform output  */
        MM_SNDBLST_WAVEIN = 7,       /*  Sound Blaster waveform input  */
        MM_ADLIB = 9,       /*  Ad Lib Compatible synth  */
        MM_MPU401_MIDIOUT = 10,      /*  MPU 401 compatible MIDI output port  */
        MM_MPU401_MIDIIN = 11,      /*  MPU 401 compatible MIDI input port  */
        MM_PC_JOYSTICK = 12,      /*  Joystick adapter  */
    }
}
