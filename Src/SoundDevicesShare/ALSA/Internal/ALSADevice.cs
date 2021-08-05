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
            NativeMethods.snd_ctl ctl = new(); 
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
                status = NativeMethods.snd_ctl_rawmidi_next_device(ref ctl, ref device);
                if (status < 0)
                {
                    Debug.WriteLine("cannot determine device number: %s", NativeMethods.snd_strerror(status));
                    break;
                }
                if (device >= 0)
                {
                    list_subdevice_info(ref ctl, card, device);
                }
            } while (device >= 0);
            NativeMethods.snd_ctl_close(ref ctl);
        }

        void list_subdevice_info(ref NativeMethods.snd_ctl ctl, int card, int device)
        {
            NativeMethods.sndrv_rawmidi_info info = new();
            string name;
            string sub_name;
            int subs, subs_in, subs_out;
            int sub;
            bool isIn, isOut;
            int status;

            //snd_rawmidi_info_alloca(&info);
            NativeMethods.snd_rawmidi_info_set_device(ref info, (uint)device);

            NativeMethods.snd_rawmidi_info_set_stream(ref info, NativeMethods.snd_rawmidi_stream.SND_RAWMIDI_STREAM_INPUT);
            NativeMethods.snd_ctl_rawmidi_info(ref ctl, ref info);
            subs_in = (int)NativeMethods.snd_rawmidi_info_get_subdevices_count(ref info);
            NativeMethods.snd_rawmidi_info_set_stream(ref info, NativeMethods.snd_rawmidi_stream.SND_RAWMIDI_STREAM_OUTPUT);
            NativeMethods.snd_ctl_rawmidi_info(ref ctl, ref info);
            subs_out = (int)NativeMethods.snd_rawmidi_info_get_subdevices_count(ref info);
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
                Debug.WriteLine("cannot get rawmidi information %d:%d: %s", card, device, NativeMethods.snd_strerror(ex.HResult));
                return;
            }
            
            name = NativeMethods.snd_rawmidi_info_get_name(ref info);
            sub_name = NativeMethods.snd_rawmidi_info_get_subdevice_name(ref info);
            if (sub_name[0] == '\0')
            {
                if (subs == 1)
                {
                    Debug.WriteLine("%c%c  hw:%d,%d    %s\n", isIn  ? 'I' : ' ', isOut ? 'O' : ' ', card, device, name);
                }
                else
                {
                    Debug.WriteLine("%c%c  hw:%d,%d    %s (%d subdevices)\n", isIn  ? 'I' : ' ', isOut ? 'O' : ' ', card, device, name, subs);
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
                    NativeMethods.snd_rawmidi_info_set_subdevice(ref info, sub);
                    if (isOut)
                    {
                        NativeMethods.snd_rawmidi_info_set_stream(ref info, NativeMethods.snd_rawmidi_stream.SND_RAWMIDI_STREAM_OUTPUT);
                        if ((status = NativeMethods.snd_ctl_rawmidi_info(ref ctl, ref info)) < 0)
                        {
                            Debug.WriteLine("cannot get rawmidi information %d:%d:%d: %s", card, device, sub, NativeMethods.snd_strerror(status));
                            break;
                        }

                    }
                    else
                    {
                        NativeMethods.snd_rawmidi_info_set_stream(ref info, NativeMethods.snd_rawmidi_stream.SND_RAWMIDI_STREAM_INPUT);
                        if ((status = NativeMethods.snd_ctl_rawmidi_info(ref ctl, ref info)) < 0)
                        {
                            Debug.WriteLine("cannot get rawmidi information %d:%d:%d: %s", card, device, sub, NativeMethods.snd_strerror(status));
                            break;
                        }
                    }
                    sub_name = NativeMethods.snd_rawmidi_info_get_subdevice_name(ref info);
                }
            }
        }




        //////////////////////////////
        //
        // is_input -- returns true if specified card/device/sub can output MIDI data.
        //

        public bool is_input(ref NativeMethods.snd_ctl ctl, int card, int device, int sub)
        {
            NativeMethods.sndrv_rawmidi_info info = new();

            NativeMethods.snd_rawmidi_info_set_device(ref info, (uint)device);
            NativeMethods.snd_rawmidi_info_set_subdevice(ref info, sub);
            NativeMethods.snd_rawmidi_info_set_stream(ref info, NativeMethods.snd_rawmidi_stream.SND_RAWMIDI_STREAM_INPUT);

            int status = NativeMethods.snd_ctl_rawmidi_info(ref ctl, ref info);
            return status switch
            {
                0 => true,
                -((int)NativeMethods.ERRNO.ENXIO) => false,
                _ => throw new Exception() { HResult = status },
            };
        }



        //////////////////////////////
        //
        // is_output -- returns true if specified card/device/sub can output MIDI data.
        //

        public bool is_output(ref NativeMethods.snd_ctl ctl, int card, int device, int sub)
        {
            NativeMethods.sndrv_rawmidi_info info = new();
            
            NativeMethods.snd_rawmidi_info_set_device(ref info, (uint)device);
            NativeMethods.snd_rawmidi_info_set_subdevice(ref info, sub);
            NativeMethods.snd_rawmidi_info_set_stream(ref info, NativeMethods.snd_rawmidi_stream.SND_RAWMIDI_STREAM_OUTPUT);

            int status = NativeMethods.snd_ctl_rawmidi_info(ref ctl, ref info);
            return status switch
            {
                0 => true,
                -((int)NativeMethods.ERRNO.ENXIO) => false,
                _ => throw new Exception() { HResult = status },
            };
        }


        public static class NativeMethods
        {
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct snd_ctl
            {
                IntPtr open_func;
                [MarshalAs(UnmanagedType.LPStr)]
                string name;
                snd_ctl_type type;
                IntPtr ops; // const snd_ctl_ops_t* ops;
                IntPtr private_data;
                int nonblock;
                int poll_fd;
                //struct list_head async_handlers;
            }

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
            }

            /*struct _snd_ctl_ops
            {
                int (* close) (snd_ctl_t* handle);
	int (* nonblock) (snd_ctl_t* handle, int nonblock);
	int (* async) (snd_ctl_t* handle, int sig, pid_t pid);
	int (* subscribe_events) (snd_ctl_t* handle, int subscribe);
	int (* card_info) (snd_ctl_t* handle, snd_ctl_card_info_t* info);
	int (* element_list) (snd_ctl_t* handle, snd_ctl_elem_list_t* list);
	int (* element_info) (snd_ctl_t* handle, snd_ctl_elem_info_t* info);
	int (* element_add) (snd_ctl_t* handle, snd_ctl_elem_info_t* info);
	int (* element_replace) (snd_ctl_t* handle, snd_ctl_elem_info_t* info);
	int (* element_remove) (snd_ctl_t* handle, snd_ctl_elem_id_t* id);
	int (* element_read) (snd_ctl_t* handle, snd_ctl_elem_value_t* control);
	int (* element_write) (snd_ctl_t* handle, snd_ctl_elem_value_t* control);
	int (* element_lock) (snd_ctl_t* handle, snd_ctl_elem_id_t*lock);
	int (* element_unlock) (snd_ctl_t* handle, snd_ctl_elem_id_t* unlock);
	int (* element_tlv) (snd_ctl_t* handle, int op_flag, unsigned int numid,
                 unsigned int* tlv, unsigned int tlv_size);
                int (* hwdep_next_device) (snd_ctl_t* handle, int* device);
	int (* hwdep_info) (snd_ctl_t* handle, snd_hwdep_info_t* info);
	int (* pcm_next_device) (snd_ctl_t* handle, int* device);
	int (* pcm_info) (snd_ctl_t* handle, snd_pcm_info_t* info);
	int (* pcm_prefer_subdevice) (snd_ctl_t* handle, int subdev);
	int (* rawmidi_next_device) (snd_ctl_t* handle, int* device);
	int (* rawmidi_info) (snd_ctl_t* handle, snd_rawmidi_info_t* info);
	int (* rawmidi_prefer_subdevice) (snd_ctl_t* handle, int subdev);
	int (* set_power_state) (snd_ctl_t* handle, unsigned int state);
                int (* get_power_state) (snd_ctl_t* handle, unsigned int* state);
                int (* read) (snd_ctl_t* handle, snd_ctl_event_t*event);

    int (* poll_descriptors_count) (snd_ctl_t* handle);
	int (* poll_descriptors) (snd_ctl_t* handle, struct pollfd *pfds, unsigned int space);
                int (* poll_revents) (snd_ctl_t* handle, struct pollfd *pfds, unsigned int nfds, unsigned short* revents);
            }*/
            

            public enum snd_ctl_type
            {
                /** Kernel level CTL */
                SND_CTL_TYPE_HW,
                /** Shared memory client CTL */
                SND_CTL_TYPE_SHM,
                /** INET client CTL (not yet implemented) */
                SND_CTL_TYPE_INET,
                /** External control plugin */
                SND_CTL_TYPE_EXT
            }
            

            public enum snd_rawmidi_type
            {
                /** Kernel level RawMidi */
                SND_RAWMIDI_TYPE_HW,
                /** Shared memory client RawMidi (not yet implemented) */
                SND_RAWMIDI_TYPE_SHM,
                /** INET client RawMidi (not yet implemented) */
                SND_RAWMIDI_TYPE_INET,
                /** Virtual (sequencer) RawMidi */
                SND_RAWMIDI_TYPE_VIRTUAL
            }

            public  enum snd_rawmidi_stream
            {
                /** Output stream */
                SND_RAWMIDI_STREAM_OUTPUT = 0,
                /** Input stream */
                SND_RAWMIDI_STREAM_INPUT,
                SND_RAWMIDI_STREAM_LAST = SND_RAWMIDI_STREAM_INPUT
            }

            public enum ERRNO
            {
                EPERM = 1,  /* Operation not permitted */
                ENOENT = 2, /* No such file or directory */
                ESRCH = 3,  /* No such process */
                EINTR = 4,  /* Interrupted system call */
                EIO = 5,    /* I/O error */
                ENXIO = 6,  /* No such device or address */
                E2BIG = 7,  /* Argument list too long */
                ENOEXEC = 8,    /* Exec format error */
                EBADF = 9,  /* Bad file number */
                ECHILD = 10,    /* No child processes */
                EAGAIN = 11,    /* Try again */
                ENOMEM = 12,    /* Out of memory */
                EACCES = 13,    /* Permission denied */
                EFAULT = 14,    /* Bad address */
                ENOTBLK = 15,   /* Block device required */
                EBUSY = 16, /* Device or resource busy */
                EEXIST = 17,    /* File exists */
                EXDEV = 18, /* Cross-device link */
                ENODEV = 19,    /* No such device */
                ENOTDIR = 20,   /* Not a directory */
                EISDIR = 21,    /* Is a directory */
                EINVAL = 22,    /* Invalid argument */
                ENFILE = 23,    /* File table overflow */
                EMFILE = 24,    /* Too many open files */
                ENOTTY = 25,    /* Not a typewriter */
                ETXTBSY = 26,   /* Text file busy */
                EFBIG = 27, /* File too large */
                ENOSPC = 28,    /* No space left on device */
                ESPIPE = 29,    /* Illegal seek */
                EROFS = 30, /* Read-only file system */
                EMLINK = 31,    /* Too many links */
                EPIPE = 32, /* Broken pipe */
                EDOM = 33,  /* Math argument out of domain of func */
                ERANGE = 34 /* Math result not representable */
            }

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

            [DllImport("libasound.so", EntryPoint = "snd_rawmidi_info_get_name", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.LPStr)]
            public static extern string snd_rawmidi_info_get_name(ref sndrv_rawmidi_info obj);

            [DllImport("libasound.so", EntryPoint = "snd_rawmidi_info_get_subdevice_name", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.LPStr)]
            public static extern string snd_rawmidi_info_get_subdevice_name(ref sndrv_rawmidi_info obj);

            [DllImport("libasound.so", EntryPoint = "snd_rawmidi_info_get_subdevices_count")]
            public static extern uint snd_rawmidi_info_get_subdevices_count(ref sndrv_rawmidi_info obj);

            [DllImport("libasound.so", EntryPoint = "snd_rawmidi_info_set_subdevice")]
            public static extern void snd_rawmidi_info_set_subdevice(ref sndrv_rawmidi_info obj, int val);


            [DllImport("libasound.so", EntryPoint = "snd_rawmidi_info_set_device")]
            public static extern void snd_rawmidi_info_set_device(ref sndrv_rawmidi_info obj, uint val);

            [DllImport("libasound.so", EntryPoint = "snd_rawmidi_info_set_stream")]
            public static extern void snd_rawmidi_info_set_stream(ref sndrv_rawmidi_info obj, snd_rawmidi_stream val);

            #endregion

            #region error

            [DllImport("libasound.so", EntryPoint = "snd_strerror", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.LPStr)]
            public static extern string snd_strerror(int errnum);

            #endregion
        }
    }
}
