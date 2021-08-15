using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.ALSA.Internal
{
    internal static class ALSAImport
    {
        [DllImport("libasound.so", EntryPoint = "snd_rawmidi_info_get_name", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string GetName(ref SndrvRawmidiInfo obj);

        [DllImport("libasound.so", EntryPoint = "snd_rawmidi_info_get_subdevice_name", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string GetSubdeviceName(ref SndrvRawmidiInfo obj);

        [DllImport("libasound.so", EntryPoint = "snd_rawmidi_info_get_subdevices_count")]
        public static extern uint GetSubdevicesCount(ref SndrvRawmidiInfo obj);

        [DllImport("libasound.so", EntryPoint = "snd_rawmidi_info_set_subdevice")]
        public static extern void SetSubdevice(ref SndrvRawmidiInfo obj, int val);


        [DllImport("libasound.so", EntryPoint = "snd_rawmidi_info_set_device")]
        public static extern void SetDevice(ref SndrvRawmidiInfo obj, uint val);

        [DllImport("libasound.so", EntryPoint = "snd_rawmidi_info_set_stream")]
        public static extern void SetStream(ref SndrvRawmidiInfo obj, SndRawmidiStream val);

    }
}
