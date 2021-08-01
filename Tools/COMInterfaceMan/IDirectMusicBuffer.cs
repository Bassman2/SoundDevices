using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    
    [ComImport]
    [Guid("d2ac2878-b39b-11d1-8704-00600893b1bd")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusicBuffer
    {

        void Flush();
        void TotalTime(ref long prtTime);
        void PackStructured(long rt, uint dwChannelGroup, uint dwChannelMessage);
        void  PackUnstructured(long rt, uint dwChannelGroup, uint cb, IntPtr lpb);
        void ResetReadPtr();
        void GetNextEvent(out long prt, out uint pdwChannelGroup, out uint pdwLength, ref IntPtr ppData);
        void GetRawBufferPtr(ref IntPtr ppData);
        void GetStartTime(out long prt);
        void GetUsedBytes(out uint pcb);
        void GetMaxBytes(out uint pcb);
        void GetBufferFormat(out Guid pGuidFormat);
        void SetStartTime(long rt);
        void SetUsedBytes(uint cb);

    }
}
