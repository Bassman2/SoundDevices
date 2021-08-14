using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO
{
    public class SoundDeviceException : Exception
    {
        public SoundDeviceException(string message) : base(message)
        { }
    }
}
