using SoundDevices.IO.ALSA.Internal;
using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Text;

namespace SoundDevices.IO.ALSA
{
    [SupportedOSPlatform("Linux")]
    public class SndException : SoundDeviceException
    {
        public SndException(string msg, int err) : base($"{msg}: {SndError.SndStrError(err)}")
        { }
    }
}
