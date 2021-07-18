using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace MediaDevices.IO.Internal.ALSA
{
    [SupportedOSPlatform("Linux")]
    internal class ALSADevice
    {

        // https://ccrma.stanford.edu/~craig/articles/linuxmidi/alsa-1.0/alsarawportlist.c


        public void GetCards()
        {
            int status;
            int card = -1;  // use -1 to prime the pump of iterating through card list
            string longname;
            string shortname;

            if ((status = NativeMethods.snd_card_next(ref card)) < 0)
            {
                Debug.WriteLine("cannot determine card number: " + NativeMethods.snd_strerror(status));
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
                if ((status = NativeMethods.snd_card_get_name(card, out shortname)) < 0)
                {
                    Debug.WriteLine("cannot determine card shortname: %s", NativeMethods.snd_strerror(status));
                    break;
                }
                if ((status = NativeMethods.snd_card_get_longname(card, out longname)) < 0)
                {
                    Debug.WriteLine("cannot determine card longname: " + NativeMethods.snd_strerror(status));
                    break;
                }
                Debug.WriteLine($"\tLONG NAME:  {longname}\n");
                Debug.WriteLine($"\tSHORT NAME: {shortname}\n");
                if ((status = NativeMethods.snd_card_next(ref card)) < 0)
                {
                    Debug.WriteLine("cannot determine card number: " + NativeMethods.snd_strerror(status));
                    break;
                }
            }

        }

        public void GetMidiPorts()
        {
            int status;
            int card = -1;  // use -1 to prime the pump of iterating through card list

            if ((status = NativeMethods.snd_card_next(ref card)) < 0)
            {
                Debug.WriteLine("cannot determine card number: %s", NativeMethods.snd_strerror(status));
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
                if ((status = NativeMethods.snd_card_next(ref card)) < 0)
                {
                    Debug.WriteLine("cannot determine card number: %s", NativeMethods.snd_strerror(status));
                    break;
                }
            }
            Debug.WriteLine("\n");
        }

        private void list_midi_devices_on_card(int card)
        {
            NativeMethods.snd_ctl ctl;
            string name;
            int device = -1;
            int status;
            name = $"hw:{card}";
            if ((status = NativeMethods.snd_ctl_open(ref ctl, ref name, 0)) < 0)
            {
                Debug.WriteLine("cannot open control for card %d: %s", card, NativeMethods.snd_strerror(status));
                return;
            }
            do
            {
                status = NativeMethods.snd_ctl_rawmidi_next_device(ctl, &device);
                if (status < 0)
                {
                    Debug.WriteLine("cannot determine device number: %s", NativeMethods.snd_strerror(status));
                    break;
                }
                if (device >= 0)
                {
                    list_subdevice_info(ctl, card, device);
                }
            } while (device >= 0);
            NativeMethods.snd_ctl_close(ctl);
        }

        void list_subdevice_info(snd_ctl_t* ctl, int card, int device)
        {
            snd_rawmidi_info_t* info;
            const char* name;
            const char* sub_name;
            int subs, subs_in, subs_out;
            int sub, in, out;
            int status;

            //snd_rawmidi_info_alloca(&info);
            NativeMethods.snd_rawmidi_info_set_device(info, device);

            NativeMethods.snd_rawmidi_info_set_stream(info, SND_RAWMIDI_STREAM_INPUT);
            NativeMethods.snd_ctl_rawmidi_info(ctl, info);
            subs_in = NativeMethods.snd_rawmidi_info_get_subdevices_count(info);
            NativeMethods.snd_rawmidi_info_set_stream(info, SND_RAWMIDI_STREAM_OUTPUT);
            NativeMethods.snd_ctl_rawmidi_info(ctl, info);
            subs_out = NativeMethods.snd_rawmidi_info_get_subdevices_count(info);
            subs = subs_in > subs_out ? subs_in : subs_out;

            sub = 0;
            in = out = 0;
            if ((status = is_output(ctl, card, device, sub)) < 0)
            {
                Debug.WriteLine("cannot get rawmidi information %d:%d: %s", card, device, snd_strerror(status));
                return;
            }
            else if (status)
            out = 1;

            if (status == 0)
            {
                if ((status = is_input(ctl, card, device, sub)) < 0)
                {
                    Debug.WriteLine("cannot get rawmidi information %d:%d: %s",
                          card, device, snd_strerror(status));
                    return;
                }
            }
            else if (status)
            { 
                in = 1;
            }
            if (status == 0)
            {
                return;
            }

            name = NativeMethods.snd_rawmidi_info_get_name(info);
            sub_name = NativeMethods.snd_rawmidi_info_get_subdevice_name(info);
            if (sub_name[0] == '\0')
            {
                if (subs == 1)
                {
                    Debug.WriteLine("%c%c  hw:%d,%d    %s\n", in  ? 'I' : ' ', out ? 'O' : ' ', card, device, name);
                }
                else
                {
                    Debug.WriteLine("%c%c  hw:%d,%d    %s (%d subdevices)\n", in  ? 'I' : ' ', out ? 'O' : ' ', card, device, name, subs);
                }
            }
            else
            {
                sub = 0;
                for (; ; )
                {
                    Debug.WriteLine("%c%c  hw:%d,%d,%d  %s\n", in ? 'I' : ' ', out ? 'O' : ' ', card, device, sub, sub_name);
                    if (++sub >= subs)
                        break;

                    in = is_input(ctl, card, device, sub);
                    out = is_output(ctl, card, device, sub);
                    NativeMethods.snd_rawmidi_info_set_subdevice(info, sub);
                    if (out) 
                    {
                    NativeMethods.snd_rawmidi_info_set_stream(info, SND_RAWMIDI_STREAM_OUTPUT);
                    if ((status = NativeMethods.snd_ctl_rawmidi_info(ctl, info)) < 0)
                    {
                        Debug.WriteLine("cannot get rawmidi information %d:%d:%d: %s", card, device, sub, snd_strerror(status));
                        break;
                    }
                }
                else
                {
                    NativeMethods.snd_rawmidi_info_set_stream(info, SND_RAWMIDI_STREAM_INPUT);
                    if ((status = NativeMethods.snd_ctl_rawmidi_info(ctl, info)) < 0)
                    {
                        Debug.WriteLine("cannot get rawmidi information %d:%d:%d: %s", card, device, sub, snd_strerror(status));
                        break;
                    }
                }
                sub_name = NativeMethods.snd_rawmidi_info_get_subdevice_name(info);
            }
        }




        //////////////////////////////
        //
        // is_input -- returns true if specified card/device/sub can output MIDI data.
        //

        public int is_input(ref NativeMethods.snd_ctl ctl, int card, int device, int sub)
        {
            NativeMethods.sndrv_rawmidi_info info;
            int status;


            NativeMethods.snd_rawmidi_info_set_device(ref info, device);
            NativeMethods.snd_rawmidi_info_set_subdevice(ref info, sub);
            NativeMethods.snd_rawmidi_info_set_stream(ref info, SND_RAWMIDI_STREAM_INPUT);

            if ((status = NativeMethods.snd_ctl_rawmidi_info(ctl, info)) < 0 && status != -ENXIO)
            {
                return status;
            }
            else if (status == 0)
            {
                return 1;
            }

            return 0;
        }



        //////////////////////////////
        //
        // is_output -- returns true if specified card/device/sub can output MIDI data.
        //

        public int is_output(ref NativeMethods.snd_ctl ctl, int card, int device, int sub)
        {
            NativeMethods.sndrv_rawmidi_info info;
            int status;


            NativeMethods.snd_rawmidi_info_set_device(ref info, device);
            NativeMethods.snd_rawmidi_info_set_subdevice(ref info, sub);
            NativeMethods.snd_rawmidi_info_set_stream(ref info, SND_RAWMIDI_STREAM_OUTPUT);

            if ((status = NativeMethods.snd_ctl_rawmidi_info(ctl, info)) < 0 && status != -ENXIO)
            {
                return status;
            }
            else if (status == 0)
            {
                return 1;
            }

            return 0;
        }


        public static class NativeMethods
        {
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct snd_ctl
            {
                IntPtr open_func;
                [MarshalAs(UnmanagedType.LPStr)]
                string name;
                snd_ctl_type_t type;
                const snd_ctl_ops_t* ops;
                IntPtr private_data;
                int nonblock;
                int poll_fd;
                struct list_head async_handlers;
            };

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct sndrv_rawmidi_info
            {
                uint device;        /* RO/WR (control): device number */
                uint subdevice;     /* RO/WR (control): subdevice number */
                int stream;         /* WR: stream */
                int card;           /* R: card number */
                uint flags;     /* SNDRV_RAWMIDI_INFO_XXXX */
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
                string id;       /* ID (user selectable) */
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
                string name;     /* name of device */
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
                string subname;  /* name of active or selected subdevice */
                uint subdevices_count;
                uint subdevices_avail;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
                string reserved; /* reserved for future use */
            };

            #region card

            [DllImport("libasound.so", EntryPoint = "snd_card_load")]
            public static extern int snd_card_load(int card);

            [DllImport("libasound.so", EntryPoint = "snd_card_next")]
            public static extern int snd_card_next(ref int card);

            [DllImport("libasound.so", EntryPoint = "snd_card_get_index")]
            public static extern int snd_card_get_index([MarshalAs(UnmanagedType.LPStr)] string name);

            [DllImport("libasound.so", EntryPoint = "snd_card_get_name", CharSet = CharSet.Ansi)]
            public static extern int snd_card_get_name(int card, [MarshalAs(UnmanagedType.LPStr)] out string name);

            [DllImport("libasound.so", EntryPoint = "snd_card_get_longname", CharSet = CharSet.Ansi)]
            public static extern int snd_card_get_longname(int card, [MarshalAs(UnmanagedType.LPStr)] out string name);

            #endregion

            #region ctl

            [DllImport("libasound.so", EntryPoint = "snd_ctl_open", CharSet = CharSet.Ansi)]
            public static extern int snd_ctl_open(ref snd_ctl ctl, [MarshalAs(UnmanagedType.LPStr)] ref string name, int mode);

            [DllImport("libasound.so", EntryPoint = "snd_ctl_close")]
            public static extern int snd_ctl_close(ref snd_ctl ctl);

            [DllImport("libasound.so", EntryPoint = "snd_ctl_rawmidi_next_device")]
            public static extern int snd_ctl_rawmidi_next_device(ref snd_ctl ctl, ref int device);

            [DllImport("libasound.so", EntryPoint = "snd_ctl_rawmidi_info")]
            public static extern int snd_ctl_rawmidi_info(ref snd_ctl ctl, ref sndrv_rawmidi_info info);

            [DllImport("libasound.so", EntryPoint = "snd_ctl_rawmidi_prefer_subdevice")]
            public static extern int snd_ctl_rawmidi_prefer_subdevice(ref snd_ctl ctl, int subdev);

            #endregion

            #region rawmidi

            [DllImport("libasound.so", EntryPoint = "snd_rawmidi_info_get_subdevices_count")]
            public static extern uint snd_rawmidi_info_get_subdevices_count(ref sndrv_rawmidi_info obj);

            [DllImport("libasound.so", EntryPoint = "snd_rawmidi_info_set_device")]
            public static extern void snd_rawmidi_info_set_device(ref sndrv_rawmidi_info obj, uint val);

            [DllImport("libasound.so", EntryPoint = "snd_rawmidi_info_set_stream")]
            public static extern void snd_rawmidi_info_set_stream(ref sndrv_rawmidi_info obj, snd_rawmidi_stream_t val);

            #endregion

            #region error

            [DllImport("libasound.so", EntryPoint = "snd_strerror", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.LPStr)]
            public static extern string snd_strerror(int errnum);

            #endregion
        }
    }
}
