
using SoundDevices.IO.WinMM.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.WinMM
{
    internal class WaveInWinMMDevice : WaveInDevice
    {
        private readonly int deviceID;
        private IntPtr deviceHandle;
        private readonly WinMMImport.Callback deviceCallback;

        internal static void AddDevices(List<WaveInDevice> devices)
        {
            int num = WinMMImport.WaveInGetNumDevs();
            for (int i = 0; i < num; i++)
            {
                try
                {
                    devices.Add(new WaveInWinMMDevice(i));
                }
                catch (SoundDeviceException ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        private WaveInWinMMDevice(int deviceID)
        {
            this.deviceID = deviceID;

            if (WinMMImport.WaveInGetDevCaps((IntPtr)deviceID, out WinMMImport.WaveInCaps waveInCaps, WinMMImport.WaveInCapsSize) != 0)
            {
                throw new SoundDeviceException("WaveInGetDevCaps failed");
            }

            this.DeviceType = SoundDeviceType.WinMM;
            this.Name = waveInCaps.name;
            this.Version = new Version(waveInCaps.driverVersion.Major, waveInCaps.driverVersion.Minor);
            this.deviceCallback = OnCallback;
        }

        private void OnCallback(IntPtr handle, WinMMMsg msg, IntPtr instance, IntPtr param1, IntPtr param2)
        {
            //throw new NotImplementedException();
        }

        #region IDisposable

        private bool disposedValue;

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    WinMMImport.WaveInReset(this.deviceHandle);
                    WinMMImport.WaveInClose(this.deviceHandle);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        #endregion

        public override void Open(WaveFormat waveFormat = null)
        {
            waveFormat = waveFormat ?? new WaveFormat();

            WinMMImport.WaveFormatEx format = new();
            format.wFormatTag = 1; // WAVE_FORMAT_PCM
            format.nChannels = (short)waveFormat.Channels;
            format.nSamplesPerSec = (short)waveFormat.SampleRate;
            format.wBitsPerSample = (short)waveFormat.BitRate;
            format.nBlockAlign = (short)(format.nChannels * format.wBitsPerSample / 8);
            format.nAvgBytesPerSec = format.nSamplesPerSec * format.nBlockAlign;
            format.cbSize = (short)WinMMImport.WaveFormatExSize;
            
            WinMMImport.WaveInOpen(out this.deviceHandle, this.deviceID, format, this.deviceCallback, IntPtr.Zero, WinMMImport.CALLBACK_FUNCTION);
        }

        public override void Start()
        {
            WinMMImport.WaveInStart(this.deviceHandle);
        }

        public override void Stop()
        {
            WinMMImport.WaveInStop(this.deviceHandle);

        }

        public override void Reset()
        {
            WinMMImport.WaveInReset(this.deviceHandle);
        }

        public override void Close()
        {
            WinMMImport.WaveInClose(this.deviceHandle);
        }
    }
}
