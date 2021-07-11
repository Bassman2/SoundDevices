using MediaDevices.IO.MIDI;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MediaDevices.IO.Internal.WinMM
{
    internal class MidiInWinMMDevice : MidiInDevice
    {
        private static readonly int sizeMidiInCaps = Marshal.SizeOf(typeof(WinMMImport.MidiInCaps));

        public static void AddInternalDevices(List<MidiInDevice> devices)
        {
            for (int i = 0; i < WinMMImport.MidiInGetNumDevs(); i++)
            {
                if (WinMMImport.MidiInGetDevCaps((IntPtr)i, out WinMMImport.MidiInCaps midiInCaps, sizeMidiInCaps) == 0)
                {
                    devices.Add(new MidiInDevice 
                    {
                        InterfaceType = SoundInterfaceType.WinMM,
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
