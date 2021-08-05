
using SoundDevices.WinMM.Internal;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.WinMM
{
    internal class WaveInWinMMDevice : WaveInDevice
    {
        private static readonly int sizeWaveInCaps = Marshal.SizeOf(typeof(WinMMImport.WaveInCaps));

        internal static void AddDevices(List<WaveInDevice> devices)
        {
            int num = WinMMImport.WaveInGetNumDevs();
            for (int i = 0; i < num; i++)
            {
                if (WinMMImport.WaveInGetDevCaps((IntPtr)i, out WinMMImport.WaveInCaps waveInCaps, sizeWaveInCaps) == 0)
                {
                    devices.Add(new WaveInDevice
                    {
                        DeviceType = SoundDeviceType.WinMM,
                        Name = waveInCaps.name,
                        Version = new Version(waveInCaps.driverVersion.Major, waveInCaps.driverVersion.Minor)
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
