﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.DirectX
{
    internal class WaveInDirectXDevice : WaveInDevice
    {
        internal static void AddDevices(List<WaveInDevice> devices)
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
