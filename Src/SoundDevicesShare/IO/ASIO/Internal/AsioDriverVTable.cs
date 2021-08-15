using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.ASIO.Internal
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

    [StructLayout(LayoutKind.Sequential)]
    class VTable
    {
        // IUnknown
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] public delegate int _AddRef(IntPtr _this);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] public delegate int _QueryInterface(IntPtr _this, Guid riid, ref IntPtr ppvObject);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] public delegate int _Release(IntPtr _this);

        // IASIO
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate int _init(IntPtr _this, IntPtr sysHandle);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate void _getDriverName(IntPtr _this, StringBuilder name);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate int _getDriverVersion(IntPtr _this);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate void _getErrorMessage(IntPtr _this, StringBuilder name);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate AsioError _start(IntPtr _this);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate AsioError _stop(IntPtr _this);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate AsioError _getChannels(IntPtr _this, out int numInputChannels, out int numOutputChannels);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate AsioError _getLatencies(IntPtr _this, out int inputLatency, out int outputLatency);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate AsioError _getBufferSize(IntPtr _this, out int minSize, out int maxSize, out int preferredSize, out int granularity);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate AsioError _canSampleRate(IntPtr _this, double sampleRate);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate AsioError _getSampleRate(IntPtr _this, out double sampleRate);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate AsioError _setSampleRate(IntPtr _this, double sampleRate);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate AsioError _getClockSources(IntPtr _this, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] AsioClockSource[] clocks, out int numSources);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate AsioError _setClockSource(IntPtr _this, int reference);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate AsioError _getSamplePosition(IntPtr _this, out long sPos, out long tStamp);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate AsioError _getChannelInfo(IntPtr _this, ref AsioChannelInfo info);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate AsioError _createBuffers(IntPtr _this, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] AsioBufferInfo[] bufferInfos, int numChannels, int bufferSize, IntPtr callbacks);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate AsioError _disposeBuffers(IntPtr _this);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate AsioError _controlPanel(IntPtr _this);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate AsioError _future(IntPtr _this, int selector, IntPtr opt);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)] public delegate AsioError _outputReady(IntPtr _this);

        // IUnknown
        public _AddRef AddRef = null;
        public _QueryInterface QueryInterface = null;
        public _Release Release = null;

        // IASIO
        public _init init = null;
        public _getDriverName getDriverName = null;
        public _getDriverVersion getDriverVersion = null;
        public _getErrorMessage getErrorMessage = null;
        public _start start = null;
        public _stop stop = null;
        public _getChannels getChannels = null;
        public _getLatencies getLatencies = null;
        public _getBufferSize getBufferSize = null;
        public _canSampleRate canSampleRate = null;
        public _getSampleRate getSampleRate = null;
        public _setSampleRate setSampleRate = null;
        public _getClockSources getClockSources = null;
        public _setClockSource setClockSource = null;
        public _getSamplePosition getSamplePosition = null;
        public _getChannelInfo getChannelInfo = null;
        public _createBuffers createBuffers = null;
        public _disposeBuffers disposeBuffers = null;
        public _controlPanel controlPanel = null;
        public _future future = null;
        public _outputReady outputReady = null;

        public VTable(IntPtr pvtbl)
        {
            FieldInfo[] fields = GetType().GetFields();
            for (int i = 0; i < fields.Length; ++i)
            {
                IntPtr pi = Marshal.ReadIntPtr(pvtbl, i * IntPtr.Size);
                fields[i].SetValue(this, Marshal.GetDelegateForFunctionPointer(pi, fields[i].FieldType));
            }
        }
    }
}
