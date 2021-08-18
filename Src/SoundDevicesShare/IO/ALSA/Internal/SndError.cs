using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace SoundDevices.IO.ALSA.Internal
{
    [SupportedOSPlatform("Linux")]
    internal static class SndError
    {
        private const string ALSALibrary = "libasound.so";

        [DllImport(ALSALibrary, EntryPoint = "snd_strerror", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string SndStrError(int errnum);
    }
}
