using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.ALSA.Internal
{
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SndCtl
    {
        IntPtr open_func;
        [MarshalAs(UnmanagedType.LPStr)]
        string name;
        SndCtlType type;
        IntPtr ops; // const snd_ctl_ops_t* ops;
        IntPtr private_data;
        int nonblock;
        int poll_fd;
        //struct list_head async_handlers;
    }
}
