using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace SoundDevices.IO.ASIO.Internal
{
    [SupportedOSPlatform("Windows")]
    internal class AsioImport
    {
        private IntPtr pAsioComObject;
        private IntPtr pinnedcallbacks;
        private AsioDriverVTable asioDriverVTable;


        public void InitFromGuid(Guid asioGuid)
        {
            const uint CLSCTX_INPROC_SERVER = 1;
            // Start to query the virtual table a index 3 (init method of AsioDriver)
            const int INDEX_VTABLE_FIRST_METHOD = 3;

            // Pointer to the ASIO object
            // USE CoCreateInstance instead of builtin COM-Class instantiation,
            // because the AsioDriver expect to have the ASIOGuid used for both COM Object and COM interface
            // The CoCreateInstance is working only in STAThread mode.
            int hresult = CoCreateInstance(ref asioGuid, IntPtr.Zero, CLSCTX_INPROC_SERVER, ref asioGuid, out pAsioComObject);
            if (hresult != 0)
            {
                throw new SoundDeviceException("Unable to instantiate ASIO. Check if STAThread is set");
            }

            // The first pointer at the adress of the ASIO Com Object is a pointer to the
            // C++ Virtual table of the object.
            // Gets a pointer to VTable.
            IntPtr pVtable = Marshal.ReadIntPtr(pAsioComObject);

            // Instantiate our Virtual table mapping
            asioDriverVTable = new AsioDriverVTable();

            // This loop is going to retrieve the pointer from the C++ VirtualTable
            // and attach an internal delegate in order to call the method on the COM Object.
            FieldInfo[] fieldInfos = typeof(AsioDriverVTable).GetFields();
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                FieldInfo fieldInfo = fieldInfos[i];
                // Read the method pointer from the VTable
                IntPtr pPointerToMethodInVTable = Marshal.ReadIntPtr(pVtable, (i + INDEX_VTABLE_FIRST_METHOD) * IntPtr.Size);
                // Instantiate a delegate
                object methodDelegate = Marshal.GetDelegateForFunctionPointer(pPointerToMethodInVTable, fieldInfo.FieldType);
                // Store the delegate in our C# VTable
                fieldInfo.SetValue(asioDriverVTable, methodDelegate);
            }
        }

        /// <summary>
        /// Inits the AsioDriver..
        /// </summary>
        /// <param name="sysHandle">The sys handle.</param>
        /// <returns></returns>
        public bool Init(IntPtr sysHandle)
        {
            int ret = asioDriverVTable.init(pAsioComObject, sysHandle);
            return ret == 1;
        }

        /// <summary>
        /// Gets the name of the driver.
        /// </summary>
        /// <returns></returns>
        public string GetDriverName()
        {
            var name = new StringBuilder(256);
            asioDriverVTable.getDriverName(pAsioComObject, name);
            return name.ToString();
        }

        /// <summary>
        /// Gets the driver version.
        /// </summary>
        /// <returns></returns>
        public int GetDriverVersion()
        {
            return asioDriverVTable.getDriverVersion(pAsioComObject);
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <returns></returns>
        public string GetErrorMessage()
        {
            var errorMessage = new StringBuilder(256);
            asioDriverVTable.getErrorMessage(pAsioComObject, errorMessage);
            return errorMessage.ToString();
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            HandleException(asioDriverVTable.start(pAsioComObject), "start");
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public AsioError Stop()
        {
            return asioDriverVTable.stop(pAsioComObject);
        }

        /// <summary>
        /// Gets the number of channels.
        /// </summary>
        /// <param name="numInputChannels">The num input channels.</param>
        /// <param name="numOutputChannels">The num output channels.</param>
        public void GetChannels(out int numInputChannels, out int numOutputChannels)
        {
            HandleException(asioDriverVTable.getChannels(pAsioComObject, out numInputChannels, out numOutputChannels), "getChannels");
        }

        /// <summary>
        /// Gets the latencies (n.b. does not throw an exception)
        /// </summary>
        /// <param name="inputLatency">The input latency.</param>
        /// <param name="outputLatency">The output latency.</param>
        public AsioError GetLatencies(out int inputLatency, out int outputLatency)
        {
            return asioDriverVTable.getLatencies(pAsioComObject, out inputLatency, out outputLatency);
        }

        /// <summary>
        /// Gets the size of the buffer.
        /// </summary>
        /// <param name="minSize">Size of the min.</param>
        /// <param name="maxSize">Size of the max.</param>
        /// <param name="preferredSize">Size of the preferred.</param>
        /// <param name="granularity">The granularity.</param>
        public void GetBufferSize(out int minSize, out int maxSize, out int preferredSize, out int granularity)
        {
            HandleException(asioDriverVTable.getBufferSize(pAsioComObject, out minSize, out maxSize, out preferredSize, out granularity), "getBufferSize");
        }

        /// <summary>
        /// Determines whether this instance can use the specified sample rate.
        /// </summary>
        /// <param name="sampleRate">The sample rate.</param>
        /// <returns>
        /// 	<c>true</c> if this instance [can sample rate] the specified sample rate; otherwise, <c>false</c>.
        /// </returns>
        public bool CanSampleRate(double sampleRate)
        {
            var error = asioDriverVTable.canSampleRate(pAsioComObject, sampleRate);
            if (error == AsioError.ASE_NoClock)
            {
                return false;
            }
            if (error == AsioError.ASE_OK)
            {
                return true;
            }
            HandleException(error, "canSampleRate");
            return false;
        }

        /// <summary>
        /// Gets the sample rate.
        /// </summary>
        /// <returns></returns>
        public double GetSampleRate()
        {
            double sampleRate;
            HandleException(asioDriverVTable.getSampleRate(pAsioComObject, out sampleRate), "getSampleRate");
            return sampleRate;
        }

        /// <summary>
        /// Sets the sample rate.
        /// </summary>
        /// <param name="sampleRate">The sample rate.</param>
        public void SetSampleRate(double sampleRate)
        {
            HandleException(asioDriverVTable.setSampleRate(pAsioComObject, sampleRate), "setSampleRate");
        }

        /// <summary>
        /// Gets the clock sources.
        /// </summary>
        /// <param name="clocks">The clocks.</param>
        /// <param name="numSources">The num sources.</param>
        public void GetClockSources(out long clocks, int numSources)
        {
            HandleException(asioDriverVTable.getClockSources(pAsioComObject, out clocks, numSources), "getClockSources");
        }

        /// <summary>
        /// Sets the clock source.
        /// </summary>
        /// <param name="reference">The reference.</param>
        public void SetClockSource(int reference)
        {
            HandleException(asioDriverVTable.setClockSource(pAsioComObject, reference), "setClockSources");
        }

        /// <summary>
        /// Gets the sample position.
        /// </summary>
        /// <param name="samplePos">The sample pos.</param>
        /// <param name="timeStamp">The time stamp.</param>
        public void GetSamplePosition(out long samplePos, ref long /*Asio64Bit*/ timeStamp)
        {
            HandleException(asioDriverVTable.getSamplePosition(pAsioComObject, out samplePos, ref timeStamp), "getSamplePosition");
        }

        /// <summary>
        /// Gets the channel info.
        /// </summary>
        /// <param name="channelNumber">The channel number.</param>
        /// <param name="trueForInputInfo">if set to <c>true</c> [true for input info].</param>
        /// <returns>Channel Info</returns>
        public AsioChannelInfo GetChannelInfo(int channelNumber, bool trueForInputInfo)
        {
            var info = new AsioChannelInfo { channel = channelNumber, isInput = trueForInputInfo };
            HandleException(asioDriverVTable.getChannelInfo(pAsioComObject, ref info), "getChannelInfo");
            return info;
        }

        /// <summary>
        /// Creates the buffers.
        /// </summary>
        /// <param name="bufferInfos">The buffer infos.</param>
        /// <param name="numChannels">The num channels.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="callbacks">The callbacks.</param>
        public void CreateBuffers(IntPtr bufferInfos, int numChannels, int bufferSize, ref AsioCallbacks callbacks)
        {
            // next two lines suggested by droidi on codeplex issue tracker
            pinnedcallbacks = Marshal.AllocHGlobal(Marshal.SizeOf(callbacks));
            Marshal.StructureToPtr(callbacks, pinnedcallbacks, false);
            HandleException(asioDriverVTable.createBuffers(pAsioComObject, bufferInfos, numChannels, bufferSize, pinnedcallbacks), "createBuffers");
        }

        /// <summary>
        /// Disposes the buffers.
        /// </summary>
        public AsioError DisposeBuffers()
        {
            AsioError result = asioDriverVTable.disposeBuffers(pAsioComObject);
            Marshal.FreeHGlobal(pinnedcallbacks);
            return result;
        }

        /// <summary>
        /// Controls the panel.
        /// </summary>
        public void ControlPanel()
        {
            HandleException(asioDriverVTable.controlPanel(pAsioComObject), "controlPanel");
        }

        /// <summary>
        /// Futures the specified selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="opt">The opt.</param>
        public void Future(int selector, IntPtr opt)
        {
            HandleException(asioDriverVTable.future(pAsioComObject, selector, opt), "future");
        }

        /// <summary>
        /// Notifies OutputReady to the AsioDriver.
        /// </summary>
        /// <returns></returns>
        public AsioError OutputReady()
        {
            return asioDriverVTable.outputReady(pAsioComObject);
        }

        /// <summary>
        /// Releases this instance.
        /// </summary>
        public void ReleaseComAsioDriver()
        {
            Marshal.Release(pAsioComObject);
        }

        /// <summary>
        /// Handles the exception. Throws an exception based on the error.
        /// </summary>
        /// <param name="error">The error to check.</param>
        /// <param name="methodName">Method name</param>
        private void HandleException(AsioError error, string methodName)
        {
            if (error != AsioError.ASE_OK && error != AsioError.ASE_SUCCESS)
            {
                //var asioException = new AsioException(
                //    $"Error code [{AsioException.getErrorName(error)}] while calling ASIO method <{methodName}>, {this.GetErrorMessage()}");
                //asioException.Error = error;
                //throw asioException;
                throw new SoundDeviceException($"Error code [{error}] while calling ASIO method <{methodName}>, {this.GetErrorMessage()}");
            }
        }

        [DllImport("ole32.Dll")] 
        private static extern int CoCreateInstance(ref Guid clsid, IntPtr inner, uint context, ref Guid uuid, out IntPtr rReturnedComObject);
    }
}
