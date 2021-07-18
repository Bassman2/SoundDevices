using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [ComImport]
    [Guid("56a86897-0ad4-11ce-b03a-0020af0ba770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IReferenceClock
    {
        void GetTime(
            REFERENCE_TIME pTime);

        void AdviseTime(
            REFERENCE_TIME baseTime,
            REFERENCE_TIME streamTime,
            IntPtr hEvent,
            uint pdwAdviseCookie);

        void AdvisePeriodic(
            REFERENCE_TIME startTime,
            REFERENCE_TIME periodTime,
            IntPtr hSemaphore,
            uint pdwAdviseCookie);

        void Unadvise(
            uint dwAdviseCookie);

    }
}
