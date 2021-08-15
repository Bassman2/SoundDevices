using SoundDevices.IO.DirectX.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.DirectX
{
    internal class WaveInDirectXDevice : WaveInDevice
    {
        internal static void AddDevices(SoundDeviceType soundDeviceType, List<WaveInDevice> devices)
        {
            DirectSoundImport.GetWaveInDevices(devices);
        }

        internal WaveInDirectXDevice(Guid deviceId, string name, string description)
        {
            this.DeviceType = SoundDeviceType.DirectX;
            this.DeviceId = deviceId;
            this.Name = name;
            this.Description = description;

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
