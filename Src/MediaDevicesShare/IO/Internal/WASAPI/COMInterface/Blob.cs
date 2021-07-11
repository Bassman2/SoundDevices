using System;
using System.Collections.Generic;
using System.Text;

namespace MediaDevices.IO.Internal.WASAPI.COMInterface
{
    /// <summary>
    /// Representation of binary large object container.
    /// </summary>
    internal struct Blob
    {
        /// <summary>
        /// Length of binary object.
        /// </summary>
        public int Length;
        /// <summary>
        /// Pointer to buffer storing data.
        /// </summary>
        public IntPtr Data;
    }
}
