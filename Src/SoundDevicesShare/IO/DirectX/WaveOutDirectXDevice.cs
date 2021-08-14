﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SoundDevices.IO.DirectX.Internal;

namespace SoundDevices.IO.DirectX
{
    internal class WaveOutDirectXDevice : WaveOutDevice
    {
        internal static void AddDevices(List<WaveOutDevice> devices) 
        {
            DirectXDevice.GetDevices(devices);
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

        public override void Open(WaveFormat waveFormat = null)
        { }

        public override void Play(Stream stream)
        {

        }

        public override void Play(byte[] buffer, int offset, int count)
        {

        }

        public override void Reset()
        { }

        public override void Close()
        { }
    }
}