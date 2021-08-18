using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace SoundDevices.IO.ALSA.Internal
{
    [SupportedOSPlatform("Linux")]
    internal static class SndRawmidiImport
    {
        private const string ALSALibrary = "libasound";

        /*
        int snd_rawmidi_open(snd_rawmidi_t** in_rmidi, snd_rawmidi_t** out_rmidi, const char* name, int mode);
        int snd_rawmidi_open_lconf(snd_rawmidi_t** in_rmidi, snd_rawmidi_t** out_rmidi, const char* name, int mode, snd_config_t *lconf);
        int snd_rawmidi_close(snd_rawmidi_t* rmidi);
        int snd_rawmidi_poll_descriptors_count(snd_rawmidi_t* rmidi);
        int snd_rawmidi_poll_descriptors(snd_rawmidi_t* rmidi, struct pollfd *pfds, unsigned int space);
        int snd_rawmidi_poll_descriptors_revents(snd_rawmidi_t* rawmidi, struct pollfd *pfds, unsigned int nfds, unsigned short* revent);
        int snd_rawmidi_nonblock(snd_rawmidi_t* rmidi, int nonblock);
        
        int snd_rawmidi_info_malloc(snd_rawmidi_info_t **ptr);
        void snd_rawmidi_info_free(snd_rawmidi_info_t *obj);
        void snd_rawmidi_info_copy(snd_rawmidi_info_t *dst, const snd_rawmidi_info_t *src);

        unsigned int snd_rawmidi_info_get_device(const snd_rawmidi_info_t *obj);
        unsigned int snd_rawmidi_info_get_subdevice(const snd_rawmidi_info_t *obj);
        snd_rawmidi_stream_t snd_rawmidi_info_get_stream(const snd_rawmidi_info_t *obj);
        int snd_rawmidi_info_get_card(const snd_rawmidi_info_t *obj);
        unsigned int snd_rawmidi_info_get_flags(const snd_rawmidi_info_t *obj);
        const char *snd_rawmidi_info_get_id(const snd_rawmidi_info_t *obj);
        */
                [DllImport(ALSALibrary, EntryPoint = "snd_rawmidi_info_get_name", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string SndRawmidiInfoGetName(ref SndrvRawmidiInfo obj);

        [DllImport(ALSALibrary, EntryPoint = "snd_rawmidi_info_get_subdevice_name", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string SndRawmidiInfoGetSubdeviceName(ref SndrvRawmidiInfo obj);

        [DllImport(ALSALibrary, EntryPoint = "snd_rawmidi_info_get_subdevices_count")]
        public static extern uint SndRawmidiInfoGetSubdevicesCount(ref SndrvRawmidiInfo obj);
        
        /*
        unsigned int snd_rawmidi_info_get_subdevices_avail(const snd_rawmidi_info_t *obj);
        */

        [DllImport(ALSALibrary, EntryPoint = "snd_rawmidi_info_set_device")]
        public static extern void SndRawmidiInfoSetDevice(ref SndrvRawmidiInfo obj, uint val);

        [DllImport(ALSALibrary, EntryPoint = "snd_rawmidi_info_set_subdevice")]
        public static extern void SndRawmidiInfoSetSubdevice(ref SndrvRawmidiInfo obj, int val);

        [DllImport(ALSALibrary, EntryPoint = "snd_rawmidi_info_set_stream")]
        public static extern void SndRawmidiInfoSetStream(ref SndrvRawmidiInfo obj, SndRawmidiStream val);

        /*
        int snd_rawmidi_info(snd_rawmidi_t* rmidi, snd_rawmidi_info_t* info);
        size_t snd_rawmidi_params_sizeof(void);

        ...
        */
    }
}
