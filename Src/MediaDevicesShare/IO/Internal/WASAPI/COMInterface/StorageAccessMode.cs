using System;
using System.Collections.Generic;
using System.Text;

namespace MediaDevices.IO.Internal.WASAPI.COMInterface
{
    /// <summary>
    /// MMDevice STGM enumeration
    /// </summary>
    internal enum StorageAccessMode
    {
        /// <summary>
        /// Read-only access mode.
        /// </summary>
        Read,
        /// <summary>
        /// Write-only access mode.
        /// </summary>
        Write,
        /// <summary>
        /// Read-write access mode.
        /// </summary>
        ReadWrite
    }
}
