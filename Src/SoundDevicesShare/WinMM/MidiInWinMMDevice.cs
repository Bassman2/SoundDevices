using SoundDevices.WinMM.Internal;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SoundDevices.WinMM
{
    internal class MidiInWinMMDevice : MidiInDevice
    {
        private static readonly int sizeMidiInCaps = Marshal.SizeOf(typeof(WinMMImport.MidiInCaps));

        internal static void AddDevices(List<MidiInDevice> devices)
        {
            for (int i = 0; i < WinMMImport.MidiInGetNumDevs(); i++)
            {
                if (WinMMImport.MidiInGetDevCaps((IntPtr)i, out WinMMImport.MidiInCaps midiInCaps, sizeMidiInCaps) == 0)
                {
                    devices.Add(new MidiInDevice 
                    {
                        DeviceType = SoundDeviceType.WinMM,
                        Name = midiInCaps.name,
                        Version = new Version(midiInCaps.driverVersion.Major, midiInCaps.driverVersion.Minor)
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
