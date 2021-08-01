using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DevicesConsole50
{
    public static class Alsa
    {
        private const string AlsaLibrary = "libasound";

        public static bool IsAlsaAvailable()
        {
            try
            {
                snd_asoundlib_version();
                return true;
            }
            catch (DllNotFoundException)
            {
                return false;
            }
            throw new Exception("unknown");
        }

        public static string SndAsoundlibVersion()
        {
            return Marshal.PtrToStringAnsi(snd_asoundlib_version());
        }

        public static string SndStrerror(int errorCode)
        {
            return Marshal.PtrToStringAnsi(snd_strerror(errorCode));
        }

        #region DllImport

        [DllImport(AlsaLibrary, EntryPoint = "snd_card_next")]
        public static extern int snd_card_next(ref int card);

        //size_t snd_ctl_elem_id_sizeof(void);

        //size_t snd_ctl_card_info_sizeof(void);

        //int snd_card_load(int card);

        //int snd_card_get_name(int card, char **name);

        [DllImport(AlsaLibrary, EntryPoint = "snd_asoundlib_version", SetLastError = true, CharSet = CharSet.Ansi)]
        //[return: MarshalAs(UnmanagedType.LPStr)]
        public static extern IntPtr snd_asoundlib_version();

        [DllImport(AlsaLibrary, EntryPoint = "snd_asoundlib_version", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.LPStr)]
        public unsafe static extern string snd_asoundlib_version2();

        [DllImport(AlsaLibrary, EntryPoint = "snd_strerror")]
        internal static extern IntPtr snd_strerror(int errnum);

        [DllImport(AlsaLibrary, EntryPoint = "snd_strerror")]
        internal static extern string snd_strerror2(int errnum);

        #endregion
    }
}
