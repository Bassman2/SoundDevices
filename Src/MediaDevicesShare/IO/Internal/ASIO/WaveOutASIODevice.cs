using MediaDevices.IO.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaDevices.IO.Internal.ASIO
{
    internal class WaveOutASIODevice : WaveOutDevice
    {
        public static void AddInternalDevices(List<WaveOutDevice> devices)
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
