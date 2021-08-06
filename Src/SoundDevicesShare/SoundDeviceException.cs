using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices
{
    public class SoundDeviceException : Exception
    {
        public SoundDeviceException(string message) : base(message)
        { }
    }
}
