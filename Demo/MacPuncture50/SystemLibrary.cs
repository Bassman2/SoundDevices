using System;
using System.Runtime.InteropServices;

namespace MacPuncture50
{
    internal static class SystemLibrary
    {
        private const string systemLibrary = "/usr/lib/libSystem.dylib";

        public static IntPtr OpenLibrary(string library, int mode = 0)
        {
            return dlopen(library, mode);
        }

        public static int CloseLibrary(IntPtr library)
        {
            return dlclose(library);
        }

        public static IntPtr GetSymbol(IntPtr handle, string symbol)
        {
            var indirect = dlsym(handle, symbol);
            return indirect == IntPtr.Zero ? indirect : Marshal.ReadIntPtr(indirect);
        }

        [DllImport(systemLibrary)]
        private static extern IntPtr dlopen(string path, int mode);

        [DllImport(systemLibrary)]
        private static extern int dlclose(IntPtr handle);
                
        [DllImport(systemLibrary)]
        private static extern IntPtr dlsym(IntPtr handle, string symbol);

        [DllImport(systemLibrary, EntryPoint = "dlerror")]
        private static extern IntPtr dlerror();
    }
}
