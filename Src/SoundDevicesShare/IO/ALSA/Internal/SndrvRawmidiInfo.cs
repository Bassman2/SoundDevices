using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.ALSA.Internal
{
    // sndrv_rawmidi_info
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SndrvRawmidiInfo
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
        uint subdevicesCount;
        uint subdevicesAvail;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        string reserved; /* reserved for future use */
    }
}
