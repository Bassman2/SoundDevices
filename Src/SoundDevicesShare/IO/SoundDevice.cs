using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO
{
    public abstract class SoundDevice : IDisposable
    {
        public SoundDeviceType DeviceType { get; internal set; }
        public Guid DeviceId { get; internal set; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public Version Version { get; internal set; }
        public string Manufacturer { get; internal set; }

        public abstract void Dispose();

        //#region IDisposable

        //private bool disposedValue;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        if (disposing)
        //        {
        //            // TODO: dispose managed state (managed objects)
        //        }

        //        // TODO: free unmanaged resources (unmanaged objects) and override finalizer
        //        // TODO: set large fields to null
        //        disposedValue = true;
        //    }
        //}

        //~SoundDevice()
        //{
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        //}

        //public void Dispose()
        //{
        //    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //    Dispose(disposing: true);
        //    GC.SuppressFinalize(this);
        //}

        //#endregion
    }
}
