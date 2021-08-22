using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.CoreAudio.Internal
{
    internal static class CoreAudioImport
    {
        //err = AudioHardwareGetProperty(kAudioHardwarePropertyDefaultOutputDevice,  &count, (void*) &device);

        //err = AudioDeviceGetProperty(device, 0, false, kAudioDevicePropertyBufferSize, &count, &bufferSize);

        //err = AudioDeviceGetProperty(device, 0, false, kAudioDevicePropertyStreamFormat, &count, &format);


        //err = AudioDeviceAddIOProc(globals->device, appIOProc, (void*) globals);    // setup our device with an IO proc

        //err = AudioDeviceStart(globals->device, appIOProc);				// start playing sound through the device

        //err = AudioDeviceStop(globals->device, appIOProc);              // stop playing sound through the device

        //err = AudioDeviceRemoveIOProc(globals->device, appIOProc);          // remove the IO proc from the device

    }
}
