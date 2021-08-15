using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace SoundDevices.IO.DirectX.Internal
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct DMUS_OBJECTDESC
    {
        int dwSize;                 /* Size of this structure. */
        int dwValidData;            /* Flags indicating which fields below are valid. */
        Guid guidObject;             /* Unique ID for this object. */
        Guid guidClass;              /* GUID for the class of object. */
        FILETIME ftDate;                 /* Last edited date of object. */
        long vVersion;               /* Version. */
        string wszName; /* Name of object. */
        string wszCategory; /* Category for object (optional). */
        string wszFileName; /* File path. */
        long llMemLength;            /* Size of Memory data. */
        IntPtr pbMemData;              /* Memory pointer for data. */
        IStream pStream;                /* Stream with data. */
    }
}
