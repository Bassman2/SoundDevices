using SoundDevices.WinMM.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.WinMM
{
    internal class WaveOutWinMMDevice : WaveOutDevice
    {
        private readonly int deviceID;
        private IntPtr deviceHandle;

        public static void AddDevices(List<WaveOutDevice> devices)
        {
            for (int i = 0; i < WinMMImport.WaveOutGetNumDevs(); i++)
            {
                try
                {
                    devices.Add(new WaveOutWinMMDevice(i));
                }
                catch (SoundDeviceException ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        private WaveOutWinMMDevice(int deviceID)
        {
            this.deviceID = deviceID;

            if (WinMMImport.WaveOutGetDevCaps((IntPtr)deviceID, out WinMMImport.WaveOutCaps waveOutCaps, WinMMImport.WaveOutCapsSize) != 0)
            {
                throw new SoundDeviceException("WaveOutGetDevCaps failed");
            }
            this.DeviceType = SoundDeviceType.WinMM;
            this.Name = waveOutCaps.name;
            this.Version = new Version(waveOutCaps.driverVersion.Major, waveOutCaps.driverVersion.Minor);
        }

        #region IDisposable

        private bool disposedValue;

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        #endregion

        public override void Open()
        { }

        public override void Reset()
        { }

        public override void Close()
        { }
    }
}
