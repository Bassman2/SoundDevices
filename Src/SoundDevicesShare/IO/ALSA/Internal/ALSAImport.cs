using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace SoundDevices.IO.ALSA.Internal
{
    // https://ccrma.stanford.edu/~craig/articles/linuxmidi/alsa-1.0/alsarawportlist.c

    [SupportedOSPlatform("Linux")]
    internal static class ALSAImport
    {
        private const string ALSALibrary = "libasound.so";

        public static void GetCards()
        {
            int status;
            int card = -1;  // use -1 to prime the pump of iterating through card list
            string longname;
            string shortname;

            if ((status = SndCardNext(ref card)) < 0)
            {
                Debug.WriteLine("cannot determine card number: " + SndStrError(status));
                return;
            }
            if (card < 0)
            {
                Debug.WriteLine("no sound cards found");
                return;
            }
            while (card >= 0)
            {
                Debug.WriteLine("Card %d:", card);
                if ((status = SndCardGetName(card, out shortname)) < 0)
                {
                    Debug.WriteLine("cannot determine card shortname: %s", SndStrError(status));
                    break;
                }
                if ((status = SndCardGetLongname(card, out longname)) < 0)
                {
                    Debug.WriteLine("cannot determine card longname: " + SndStrError(status));
                    break;
                }
                Debug.WriteLine($"\tLONG NAME:  {longname}\n");
                Debug.WriteLine($"\tSHORT NAME: {shortname}\n");
                if ((status = SndCardNext(ref card)) < 0)
                {
                    Debug.WriteLine("cannot determine card number: " + SndStrError(status));
                    break;
                }
            }

        }

        public static void GetMidiPorts()
        {
            int status;
            int card = -1;  // use -1 to prime the pump of iterating through card list

            if ((status = SndCardNext(ref card)) < 0)
            {
                Debug.WriteLine("cannot determine card number: %s", SndStrError(status));
                return;
            }
            if (card < 0)
            {
                Debug.WriteLine("no sound cards found");
                return;
            }
            Debug.WriteLine("\nDir Device    Name\n");
            Debug.WriteLine("====================================\n");
            while (card >= 0)
            {
                list_midi_devices_on_card(card);
                if ((status = SndCardNext(ref card)) < 0)
                {
                    Debug.WriteLine("cannot determine card number: %s", SndStrError(status));
                    break;
                }
            }
            Debug.WriteLine("\n");
        }

        private static void list_midi_devices_on_card(int card)
        {
            SndCtl ctl = new();
            string name;
            int device = -1;
            int status;
            name = $"hw:{card}";
            if ((status = SndCtlOpen(ref ctl, ref name, 0)) < 0)
            {
                Debug.WriteLine("cannot open control for card %d: %s", card, SndStrError(status));
                return;
            }
            do
            {
                status = SndCtlRawmidiNextDevice(ref ctl, ref device);
                if (status < 0)
                {
                    Debug.WriteLine("cannot determine device number: %s", SndStrError(status));
                    break;
                }
                if (device >= 0)
                {
                    list_subdevice_info(ref ctl, card, device);
                }
            } while (device >= 0);
            SndCtlClose(ref ctl);
        }

        static void list_subdevice_info(ref SndCtl ctl, int card, int device)
        {
            SndrvRawmidiInfo info = new();
            string name;
            string sub_name;
            int subs, subs_in, subs_out;
            int sub;
            bool isIn, isOut;
            int status;

            //snd_rawmidi_info_alloca(&info);
            SndRawmidiInfoSetSubdevice(ref info, device);

            SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Input);
            SndCtlRawmidiInfo(ref ctl, ref info);
            subs_in = (int)SndRawmidiInfoGetSubdevicesCount(ref info);
            SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Output);
            SndCtlRawmidiInfo(ref ctl, ref info);
            subs_out = (int)SndRawmidiInfoGetSubdevicesCount(ref info);
            subs = subs_in > subs_out ? subs_in : subs_out;

            sub = 0;
            isIn = isOut = false;
            try
            {
                isOut = is_output(ref ctl, card, device, sub);
                isIn = is_input(ref ctl, card, device, sub);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("cannot get rawmidi information %d:%d: %s", card, device, SndStrError(ex.HResult));
                return;
            }

            name = SndRawmidiInfoGetName(ref info);
            sub_name = SndRawmidiInfoGetSubdeviceName(ref info);
            if (sub_name[0] == '\0')
            {
                if (subs == 1)
                {
                    Debug.WriteLine("%c%c  hw:%d,%d    %s\n", isIn ? 'I' : ' ', isOut ? 'O' : ' ', card, device, name);
                }
                else
                {
                    Debug.WriteLine("%c%c  hw:%d,%d    %s (%d subdevices)\n", isIn ? 'I' : ' ', isOut ? 'O' : ' ', card, device, name, subs);
                }
            }
            else
            {
                sub = 0;
                for (; ; )
                {
                    Debug.WriteLine("%c%c  hw:%d,%d,%d  %s\n", isIn ? 'I' : ' ', isOut ? 'O' : ' ', card, device, sub, sub_name);
                    if (++sub >= subs)
                        break;

                    isIn = is_input(ref ctl, card, device, sub);
                    isOut = is_output(ref ctl, card, device, sub);
                    SndRawmidiInfoSetSubdevice(ref info, sub);
                    if (isOut)
                    {
                        SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Output);
                        if ((status = SndCtlRawmidiInfo(ref ctl, ref info)) < 0)
                        {
                            Debug.WriteLine("cannot get rawmidi information %d:%d:%d: %s", card, device, sub, SndStrError(status));
                            break;
                        }

                    }
                    else
                    {
                        SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Input);
                        if ((status = SndCtlRawmidiInfo(ref ctl, ref info)) < 0)
                        {
                            Debug.WriteLine("cannot get rawmidi information %d:%d:%d: %s", card, device, sub, SndStrError(status));
                            break;
                        }
                    }
                    sub_name = SndRawmidiInfoGetSubdeviceName(ref info);
                }
            }
        }




        //////////////////////////////
        //
        // is_input -- returns true if specified card/device/sub can output MIDI data.
        //

        public static bool is_input(ref SndCtl ctl, int card, int device, int sub)
        {
            SndrvRawmidiInfo info = new();

            SndRawmidiInfoSetDevice(ref info, (uint)device);
            SndRawmidiInfoSetSubdevice(ref info, sub);
            SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Input);

            int status = SndCtlRawmidiInfo(ref ctl, ref info);
            return status switch
            {
                0 => true,
                -((int)ERRNO.ENXIO) => false,
                _ => throw new Exception() { HResult = status },
            };
        }



        //////////////////////////////
        //
        // is_output -- returns true if specified card/device/sub can output MIDI data.
        //

        public static bool is_output(ref SndCtl ctl, int card, int device, int sub)
        {
            SndrvRawmidiInfo info = new();

            SndRawmidiInfoSetDevice(ref info, (uint)device);
            SndRawmidiInfoSetSubdevice(ref info, sub);
            SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Output);

            int status = SndCtlRawmidiInfo(ref ctl, ref info);
            return status switch
            {
                0 => true,
                -((int)ERRNO.ENXIO) => false,
                _ => throw new Exception() { HResult = status },
            };
        }



        [DllImport("libasound.so", EntryPoint = "snd_card_load")]
        public static extern int SndCardLoad(int card);

        [DllImport("libasound.so", EntryPoint = "snd_card_next")]
        public static extern int SndCardNext(ref int card);

        [DllImport("libasound.so", EntryPoint = "snd_card_get_index")]
        public static extern int SndCardGetIndex([MarshalAs(UnmanagedType.LPStr)] string name);

        [DllImport("libasound.so", EntryPoint = "snd_card_get_name", CharSet = CharSet.Ansi)]
        public static extern int SndCardGetName(int card, [MarshalAs(UnmanagedType.LPStr)] out string name);

        [DllImport("libasound.so", EntryPoint = "snd_card_get_longname", CharSet = CharSet.Ansi)]
        public static extern int SndCardGetLongname(int card, [MarshalAs(UnmanagedType.LPStr)] out string name);




        [DllImport("libasound.so", EntryPoint = "snd_ctl_open", CharSet = CharSet.Ansi)]
        public static extern int SndCtlOpen(ref SndCtl ctl, [MarshalAs(UnmanagedType.LPStr)] ref string name, int mode);

        [DllImport("libasound.so", EntryPoint = "snd_ctl_close")]
        public static extern int SndCtlClose(ref SndCtl ctl);

        [DllImport("libasound.so", EntryPoint = "snd_ctl_rawmidi_next_device")]
        public static extern int SndCtlRawmidiNextDevice(ref SndCtl ctl, ref int device);

        [DllImport("libasound.so", EntryPoint = "snd_ctl_rawmidi_info")]
        public static extern int SndCtlRawmidiInfo(ref SndCtl ctl, ref SndrvRawmidiInfo info);

        [DllImport("libasound.so", EntryPoint = "snd_ctl_rawmidi_prefer_subdevice")]
        public static extern int SndCtlRawmidiPreferSubdevice(ref SndCtl ctl, int subdev);




        [DllImport(ALSALibrary, EntryPoint = "snd_rawmidi_info_get_name", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string SndRawmidiInfoGetName(ref SndrvRawmidiInfo obj);

        [DllImport(ALSALibrary, EntryPoint = "snd_rawmidi_info_get_subdevice_name", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string SndRawmidiInfoGetSubdeviceName(ref SndrvRawmidiInfo obj);

        [DllImport(ALSALibrary, EntryPoint = "snd_rawmidi_info_get_subdevices_count")]
        public static extern uint SndRawmidiInfoGetSubdevicesCount(ref SndrvRawmidiInfo obj);

        [DllImport(ALSALibrary, EntryPoint = "snd_rawmidi_info_set_subdevice")]
        public static extern void SndRawmidiInfoSetSubdevice(ref SndrvRawmidiInfo obj, int val);


        [DllImport(ALSALibrary, EntryPoint = "snd_rawmidi_info_set_device")]
        public static extern void SndRawmidiInfoSetDevice(ref SndrvRawmidiInfo obj, uint val);

        [DllImport(ALSALibrary, EntryPoint = "snd_rawmidi_info_set_stream")]
        public static extern void SndRawmidiInfoSetStream(ref SndrvRawmidiInfo obj, SndRawmidiStream val);
        
        [DllImport(ALSALibrary, EntryPoint = "snd_strerror", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string SndStrError(int errnum);

    }
}
