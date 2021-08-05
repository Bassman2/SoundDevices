using SoundDevices.WinMM.Internal;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.WinMM
{
    internal class WaveOutWinMMDevice : WaveOutDevice
    {
        private static readonly int sizeWaveOutCaps = Marshal.SizeOf(typeof(WinMMImport.WaveOutCaps));

        public static void AddDevices(List<WaveOutDevice> devices)
        {
            for (int i = 0; i < WinMMImport.WaveOutGetNumDevs(); i++)
            {
                if (WinMMImport.WaveOutGetDevCaps((IntPtr)i, out WinMMImport.WaveOutCaps waveOutCaps, sizeWaveOutCaps) == 0)
                {
                    devices.Add(new WaveOutDevice
                    {
                        DeviceType = SoundDeviceType.WinMM,
                        Name = waveOutCaps.name,
                        Version = new Version(waveOutCaps.driverVersion.Major, waveOutCaps.driverVersion.Minor)
                    });
                }
            }
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
    }
}
