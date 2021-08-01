using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    //[ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IReferenceClock
    {
        void GetTime(out long pTime);
        void AdviseTime(long baseTime, long streamTime, IntPtr hEvent, out uint pdwAdviseCookie);
        void AdvisePeriodic(long startTime, long periodTime, IntPtr hSemaphore, out uint pdwAdviseCookie);
        void Unadvise(uint dwAdviseCookie);
    }
}
