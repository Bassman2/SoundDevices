using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace SoundDevices.IO.ALSA.Internal
{
    [SupportedOSPlatform("Linux")]
    internal static class SndCtlImport
    {
        private const string ALSALibrary = "libasound";

        [DllImport(ALSALibrary, EntryPoint = "snd_card_load")]
        public static extern int SndCardLoad(int card);

        [DllImport(ALSALibrary, EntryPoint = "snd_card_next")]
        public static extern int SndCardNext(ref int card);

        [DllImport(ALSALibrary, EntryPoint = "snd_card_get_index")]
        public static extern int SndCardGetIndex([MarshalAs(UnmanagedType.LPStr)] string name);

        [DllImport(ALSALibrary, EntryPoint = "snd_card_get_name", CharSet = CharSet.Ansi)]
        public static extern int SndCardGetName(int card, [MarshalAs(UnmanagedType.LPStr)] out string name);

        [DllImport(ALSALibrary, EntryPoint = "snd_card_get_longname", CharSet = CharSet.Ansi)]
        public static extern int SndCardGetLongname(int card, [MarshalAs(UnmanagedType.LPStr)] out string name);

        /*
        int snd_device_name_hint(int card, const char *iface, void ***hints);
        int snd_device_name_free_hint(void **hints);
        char *snd_device_name_get_hint(const void *hint, const char *id);
        */

        [DllImport(ALSALibrary, EntryPoint = "snd_ctl_open", CharSet = CharSet.Ansi)]
        public static extern int SndCtlOpen(ref SndCtl ctl, [MarshalAs(UnmanagedType.LPStr)] ref string name, int mode);

        /*
        int snd_ctl_open_lconf(snd_ctl_t** ctl, const char* name, int mode, snd_config_t *lconf);
        int snd_ctl_open_fallback(snd_ctl_t** ctl, snd_config_t* root, const char* name, const char* orig_name, int mode);
        */

        [DllImport(ALSALibrary, EntryPoint = "snd_ctl_close")]
        public static extern int SndCtlClose(ref SndCtl ctl);

        /*
        int snd_ctl_nonblock(snd_ctl_t* ctl, int nonblock);
        static __inline__ int snd_ctl_abort(snd_ctl_t* ctl) { return snd_ctl_nonblock(ctl, 2); }
        int snd_async_add_ctl_handler(snd_async_handler_t** handler, snd_ctl_t* ctl,
                          snd_async_callback_t callback, void* private_data);
        snd_ctl_t* snd_async_handler_get_ctl(snd_async_handler_t* handler);
        int snd_ctl_poll_descriptors_count(snd_ctl_t* ctl);
        int snd_ctl_poll_descriptors(snd_ctl_t* ctl, struct pollfd *pfds, unsigned int space);
        int snd_ctl_poll_descriptors_revents(snd_ctl_t* ctl, struct pollfd *pfds, unsigned int nfds, unsigned short* revents);
        int snd_ctl_subscribe_events(snd_ctl_t* ctl, int subscribe);
        int snd_ctl_card_info(snd_ctl_t* ctl, snd_ctl_card_info_t* info);
        int snd_ctl_elem_list(snd_ctl_t* ctl, snd_ctl_elem_list_t* list);
        int snd_ctl_elem_info(snd_ctl_t* ctl, snd_ctl_elem_info_t* info);
        int snd_ctl_elem_read(snd_ctl_t* ctl, snd_ctl_elem_value_t* data);
        int snd_ctl_elem_write(snd_ctl_t* ctl, snd_ctl_elem_value_t* data);
        int snd_ctl_elem_lock(snd_ctl_t* ctl, snd_ctl_elem_id_t* id);
        int snd_ctl_elem_unlock(snd_ctl_t* ctl, snd_ctl_elem_id_t* id);
        int snd_ctl_elem_tlv_read(snd_ctl_t* ctl, const snd_ctl_elem_id_t* id,
                      unsigned int* tlv, unsigned int tlv_size);
        int snd_ctl_elem_tlv_write(snd_ctl_t* ctl, const snd_ctl_elem_id_t* id,

               const unsigned int* tlv);
        int snd_ctl_elem_tlv_command(snd_ctl_t* ctl, const snd_ctl_elem_id_t* id,

                 const unsigned int* tlv);
        #ifdef __ALSA_HWDEP_H
        int snd_ctl_hwdep_next_device(snd_ctl_t *ctl, int * device);
        int snd_ctl_hwdep_info(snd_ctl_t *ctl, snd_hwdep_info_t * info);
        #endif
        #ifdef __ALSA_PCM_H
        int snd_ctl_pcm_next_device(snd_ctl_t *ctl, int *device);
        int snd_ctl_pcm_info(snd_ctl_t *ctl, snd_pcm_info_t * info);
        int snd_ctl_pcm_prefer_subdevice(snd_ctl_t *ctl, int subdev);
        #endif
        */

        [DllImport(ALSALibrary, EntryPoint = "snd_ctl_rawmidi_next_device")]
        public static extern int SndCtlRawmidiNextDevice(ref SndCtl ctl, ref int device);

        [DllImport(ALSALibrary, EntryPoint = "snd_ctl_rawmidi_info")]
        public static extern int SndCtlRawmidiInfo(ref SndCtl ctl, ref SndrvRawmidiInfo info);

        [DllImport(ALSALibrary, EntryPoint = "snd_ctl_rawmidi_prefer_subdevice")]
        public static extern int SndCtlRawmidiPreferSubdevice(ref SndCtl ctl, int subdev);


    }
}
