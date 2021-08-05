using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.ALSA
{
    internal class WaveInALSADevice : WaveInDevice
    {
        internal static void AddDevices(List<WaveInDevice> devices)
        {
            
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
