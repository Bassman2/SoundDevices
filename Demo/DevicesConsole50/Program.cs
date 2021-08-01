using System;
using System.Runtime.InteropServices;

namespace DevicesConsole50
{
    class Program
    {
        // on Linux
        // dotnet build --runtime ubuntu.20.04-x64
        // dotnet run

        // search path: A colon-separated list of directories in the user’s LD_LIBRARY_PATH environment variable.

        // check libs for unresolved symbols
        // ldd -r -d libasound.so

        // /lib/x86_64-linux-gnu/libasound.so.2

        // https://developers.redhat.com/blog/2019/09/06/interacting-with-native-libraries-in-net-core-3-0

        // NativeLibrary

        // /etc/ld.so.cache

        // https://www.mono-project.com/docs/advanced/pinvoke/

        // http://www.swig.org/

        private const string AlsaLibrary = "libasound";

        static void Main(string[] args)
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnUnhandledException);
                new Program().Test();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exc: {ex.ToString()}");
            }
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Exception ex = (Exception)args.ExceptionObject;
            Console.WriteLine($"UnhandledException: {ex}");
        }

        public static bool IsAlsaInstalled()
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

        public void Test()
        {
            try
            {
                if (OperatingSystem.IsLinux())
                {
                    if (!Alsa.IsAlsaAvailable())
                    {
                        Console.WriteLine("Alsa not installed");
                        return;
                    }

                    string alsaVersion = Alsa.SndAsoundlibVersion();
                    Console.WriteLine($"ALSA Version: {alsaVersion}");

                    int cardIndex = -1;
                    int status = Alsa.snd_card_next(ref cardIndex);
                    Console.WriteLine($"status: {status} cardIndex: {cardIndex}");

                    //if (!IsAlsaInstalled())
                    //{
                    //    Console.WriteLine("Alsa not installed");
                    //    return;
                    //}

                    //Console.WriteLine("zHello World!");

                    //IntPtr ptr = snd_asoundlib_version();
                    //string version = Marshal.PtrToStringAnsi(ptr);
                    //Console.WriteLine(version);

                    for (int er = 0; er < 50; er++)
                    {
                        string err = Alsa.SndStrerror(er);
                        Console.WriteLine($"Error code {er}: {err}");
                    }
                    ////string err2 = snd_strerror2(5);
                    ////Console.WriteLine(err2);

                    //string version2 = snd_asoundlib_version2();
                    ////int err = Marshal.GetLastWin32Error();
                    ////Console.WriteLine($"error: {err}");
                    //Console.WriteLine(version2);

                    Console.WriteLine("======================");
                }
                else
                {
                    Console.WriteLine("No Linux");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exc: {ex}");
            }
        }

        

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
    }
}
