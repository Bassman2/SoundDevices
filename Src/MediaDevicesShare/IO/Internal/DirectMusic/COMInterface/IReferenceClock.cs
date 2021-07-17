using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [ComImport]
    [Guid("d2ac2878-b39b-11d1-8704-00600893b1bd")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IReferenceClock
    {
        void GetTime(
            REFERENCE_TIME baseTime,
            REFERENCE_TIME baseTime,
            REFERENCE_TIME streamTime,
            IntPtr hEvent,
            pdwAdviseCooki e);

        void AdvisePeriodic(
            REFERENCE_TIME startTime,
            REFERENCE_TIME periodTime,
            IntPtr hSemaphore,
            pdwAdviseCooki e);

        void Unadvise(
            uint dwAdviseCookie);

    }
}
