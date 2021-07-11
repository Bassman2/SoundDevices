using MediaDevices.IO.Wave;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaDevices.IO.Internal.WinMM
{
    internal class WaveInWinMMDevice : WaveInDevice
    {
        private static readonly int sizeWaveInCaps = Marshal.SizeOf(typeof(WinMMImport.WaveInCaps));

        public static void AddInternalDevices(List<WaveInDevice> devices)
        {
            int num = WinMMImport.WaveInGetNumDevs();
            for (int i = 0; i < num; i++)
            {
                if (WinMMImport.WaveInGetDevCaps((IntPtr)i, out WinMMImport.WaveInCaps waveInCaps, sizeWaveInCaps) == 0)
                {
                    devices.Add(new WaveInDevice
                    {
                        InterfaceType = SoundInterfaceType.WinMM,
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
