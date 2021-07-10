using MediaDevices.IO.Wave.Internal.WinMM;
using System;
using System.Collections.Generic;

namespace MediaDevices.IO.Wave
{
    public class WaveOutDevice : IDisposable
    {
        public static WaveOutDeviceInfo[] GetDevices(WaveDeviceTypes waveDeviceTypes)
        {
            List<WaveOutDeviceInfo> devices = new();
            if (OperatingSystem.IsWindows() && waveDeviceTypes.HasFlag(WaveDeviceTypes.WinMM))
            {
                devices.AddRange(WaveOutWinMMDevice.GetDevices());
            }
            if (OperatingSystem.IsAndroid())
            {

            }
            if (OperatingSystem.IsLinux())
            {

            }
            if (OperatingSystem.IsIOS())
            {

            }
            if (OperatingSystem.IsMacOS())
            {

            }
            return devices.ToArray();
        }



        #region IDisposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
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

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~MidiInDevice()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
   
    }
}
