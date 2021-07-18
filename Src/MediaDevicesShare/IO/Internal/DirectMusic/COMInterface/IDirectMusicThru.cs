using System;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.DirectMusic.COMInterface
{
    [ComImport]
    [Guid("ced153e7-3606-11d2-b9f9-0000f875ac12")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusicThru
    {
        void ThruChannel(
            uint dwSourceChannelGroup,
            uint dwSourceChannel,
            uint dwDestinationChannelGroup,
            uint dwDestinationChannel,
            ref IDirectMusicPort pDestinationPort);

    }
}
