
using SoundDevices.WinMM.Internal;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SoundDevices.WinMM
{
    internal class MidiOutWinMMDevice : MidiOutDevice
    {
        private static readonly int sizeMidiOutCaps = Marshal.SizeOf(typeof(WinMMImport.MidiOutCaps));

        internal static void AddDevices(List<MidiOutDevice> devices)
        {
            for (int i = 0; i < WinMMImport.MidiOutGetNumDevs(); i++)
            {
                if (WinMMImport.MidiOutGetDevCaps((IntPtr)i, out WinMMImport.MidiOutCaps midiOutCaps, sizeMidiOutCaps) == 0)
                {
                    devices.Add(new MidiOutDevice
                    {
                        DeviceType = SoundDeviceType.WinMM,
                        Name = midiOutCaps.name,
                        Version = new Version(midiOutCaps.driverVersion.Major, midiOutCaps.driverVersion.Minor)
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
