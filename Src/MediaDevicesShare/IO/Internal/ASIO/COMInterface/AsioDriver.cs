using System;
using System.Collections.Generic;
using System.Text;

namespace MediaDevices.IO.Internal.ASIO.COMInterface
{
    internal class AsioDriver
    {
            //ASIOBool init(void*);
            //void getDriverName(char* name);
            //long getDriverVersion();
            //void getErrorMessage(char*string);
            //ASIOError start();
            //ASIOError stop();
            //ASIOError getChannels(long* numInputChannels, long* numOutputChannels);
            //ASIOError getLatencies(long* inputLatency, long* outputLatency);
            //ASIOError getBufferSize(long* minSize, long* maxSize, long* preferredSize, long* granularity);
            //ASIOError canSampleRate(ASIOSampleRate sampleRate);
            //ASIOError getSampleRate(ASIOSampleRate* sampleRate);
            //ASIOError setSampleRate(ASIOSampleRate sampleRate);
            //ASIOError getClockSources(ASIOClockSource* clocks, long* numSources);
            //ASIOError setClockSource(long reference);
            //ASIOError getSamplePosition(ASIOSamples* sPos, ASIOTimeStamp* tStamp);
            //ASIOError getChannelInfo(ASIOChannelInfo* info);
            //ASIOError createBuffers(ASIOBufferInfo* bufferInfos, long numChannels,
            //long bufferSize, ASIOCallbacks* callbacks);
            //ASIOError disposeBuffers();
            //ASIOError controlPanel();
            //ASIOError future(long selector, void* opt);
            //ASIOError outputReady();
        //};
    }
}
