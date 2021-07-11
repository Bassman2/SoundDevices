using MediaDevices.IO.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaDevices.IO.Internal.DirectSound
{
    internal class WaveInDirectSoundDevice : WaveInDevice
    {
        public static void AddInternalDevices(List<WaveInDevice> devices)
        {
            //DirectX.GetDevices(devices);
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
