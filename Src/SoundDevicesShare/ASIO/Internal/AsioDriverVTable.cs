using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.ASIO.Internal
{
    
    /// <summary>
    /// Internal VTable structure to store all the delegates to the C++ COM method.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class AsioDriverVTable
    {
        //3  virtual ASIOBool init(void *sysHandle) = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int ASIOInit(IntPtr _pUnknown, IntPtr sysHandle);
        public ASIOInit init = null;
        //4  virtual void getDriverName(char *name) = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void ASIOgetDriverName(IntPtr _pUnknown, StringBuilder name);
        public ASIOgetDriverName getDriverName = null;
        //5  virtual long getDriverVersion() = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int ASIOgetDriverVersion(IntPtr _pUnknown);
        public ASIOgetDriverVersion getDriverVersion = null;
        //6  virtual void getErrorMessage(char *string) = 0;	
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void ASIOgetErrorMessage(IntPtr _pUnknown, StringBuilder errorMessage);
        public ASIOgetErrorMessage getErrorMessage = null;
        //7  virtual ASIOError start() = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate AsioError ASIOstart(IntPtr _pUnknown);
        public ASIOstart start = null;
        //8  virtual ASIOError stop() = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate AsioError ASIOstop(IntPtr _pUnknown);
        public ASIOstop stop = null;
        //9  virtual ASIOError getChannels(long *numInputChannels, long *numOutputChannels) = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate AsioError ASIOgetChannels(IntPtr _pUnknown, out int numInputChannels, out int numOutputChannels);
        public ASIOgetChannels getChannels = null;
        //10  virtual ASIOError getLatencies(long *inputLatency, long *outputLatency) = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate AsioError ASIOgetLatencies(IntPtr _pUnknown, out int inputLatency, out int outputLatency);
        public ASIOgetLatencies getLatencies = null;
        //11 virtual ASIOError getBufferSize(long *minSize, long *maxSize, long *preferredSize, long *granularity) = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate AsioError ASIOgetBufferSize(IntPtr _pUnknown, out int minSize, out int maxSize, out int preferredSize, out int granularity);
        public ASIOgetBufferSize getBufferSize = null;
        //12 virtual ASIOError canSampleRate(ASIOSampleRate sampleRate) = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate AsioError ASIOcanSampleRate(IntPtr _pUnknown, double sampleRate);
        public ASIOcanSampleRate canSampleRate = null;
        //13 virtual ASIOError getSampleRate(ASIOSampleRate *sampleRate) = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate AsioError ASIOgetSampleRate(IntPtr _pUnknown, out double sampleRate);
        public ASIOgetSampleRate getSampleRate = null;
        //14 virtual ASIOError setSampleRate(ASIOSampleRate sampleRate) = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate AsioError ASIOsetSampleRate(IntPtr _pUnknown, double sampleRate);
        public ASIOsetSampleRate setSampleRate = null;
        //15 virtual ASIOError getClockSources(ASIOClockSource *clocks, long *numSources) = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate AsioError ASIOgetClockSources(IntPtr _pUnknown, out long clocks, int numSources);
        public ASIOgetClockSources getClockSources = null;
        //16 virtual ASIOError setClockSource(long reference) = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate AsioError ASIOsetClockSource(IntPtr _pUnknown, int reference);
        public ASIOsetClockSource setClockSource = null;
        //17 virtual ASIOError getSamplePosition(ASIOSamples *sPos, ASIOTimeStamp *tStamp) = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate AsioError ASIOgetSamplePosition(IntPtr _pUnknown, out long samplePos, ref long /*Asio64Bit*/ timeStamp);
        public ASIOgetSamplePosition getSamplePosition = null;
        //18 virtual ASIOError getChannelInfo(ASIOChannelInfo *info) = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate AsioError ASIOgetChannelInfo(IntPtr _pUnknown, ref AsioChannelInfo info);
        public ASIOgetChannelInfo getChannelInfo = null;
        //19 virtual ASIOError createBuffers(ASIOBufferInfo *bufferInfos, long numChannels, long bufferSize, ASIOCallbacks *callbacks) = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        //            public delegate ASIOError ASIOcreateBuffers(IntPtr _pUnknown, ref ASIOBufferInfo[] bufferInfos, int numChannels, int bufferSize, ref ASIOCallbacks callbacks);
        public delegate AsioError ASIOcreateBuffers(IntPtr _pUnknown, IntPtr bufferInfos, int numChannels, int bufferSize, IntPtr callbacks);
        public ASIOcreateBuffers createBuffers = null;
        //20 virtual ASIOError disposeBuffers() = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate AsioError ASIOdisposeBuffers(IntPtr _pUnknown);
        public ASIOdisposeBuffers disposeBuffers = null;
        //21 virtual ASIOError controlPanel() = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate AsioError ASIOcontrolPanel(IntPtr _pUnknown);
        public ASIOcontrolPanel controlPanel = null;
        //22 virtual ASIOError future(long selector,void *opt) = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate AsioError ASIOfuture(IntPtr _pUnknown, int selector, IntPtr opt);
        public ASIOfuture future = null;
        //23 virtual ASIOError outputReady() = 0;
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate AsioError ASIOoutputReady(IntPtr _pUnknown);
        public ASIOoutputReady outputReady = null;
    }
}
