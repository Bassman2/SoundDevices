
using SoundDevices.WinMM.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.WinMM
{
    internal class WaveInWinMMDevice : WaveInDevice
    {
        private readonly int deviceID;
        private IntPtr deviceHandle;
                
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

        public override void Open()
        { }

        public override void Start()
        { }

        public override void Stop()
        { }

        public override void Reset()
        { }

        public override void Close()
        { }
    }
}
