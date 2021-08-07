using SoundDevices.WinMM.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SoundDevices.WinMM
{
    internal class MidiInWinMMDevice : MidiInDevice
    {
        private readonly int deviceID;
        private IntPtr deviceHandle;
        private readonly WinMMImport.MidiInProc midiInProc;

        internal static void AddDevices(List<MidiInDevice> devices)
        {
            for (int i = 0; i < WinMMImport.MidiInGetNumDevs(); i++)
            {
                try
                {
                    devices.Add(new MidiInWinMMDevice(i));
                }
                catch (SoundDeviceException ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        private MidiInWinMMDevice(int deviceID)
        {
            this.deviceID = deviceID;
            this.midiInProc = HandleMessage;

            if (WinMMImport.MidiInGetDevCaps((IntPtr)deviceID, out WinMMImport.MidiInCaps midiInCaps, WinMMImport.MidiInCapsSize) != 0)
            {
                throw new SoundDeviceException("MidiInGetDevCaps failed");
            }
            
            this.DeviceType = SoundDeviceType.WinMM;
            this.Name = midiInCaps.name;
            this.Version = new Version(midiInCaps.driverVersion.Major, midiInCaps.driverVersion.Minor);

        }

        #region IDisposable

        private bool disposedValue;

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    WinMMImport.MidiInReset(this.deviceHandle);
                    WinMMImport.MidiInClose(this.deviceHandle);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        #endregion

        public override void Open()
        {
            
            int result = WinMMImport.MidiInOpen(out this.deviceHandle, this.deviceID, this.midiInProc, IntPtr.Zero, WinMMImport.CALLBACK_FUNCTION);

        }

        private void HandleMessage(IntPtr hnd, int msg, IntPtr instance, IntPtr param1, IntPtr param2)
        {
            switch ((WinMMInMsg)msg)
            {
            case WinMMInMsg.MIM_OPEN:
                break;
            case WinMMInMsg.MIM_CLOSE:
                break;
            case WinMMInMsg.MIM_DATA:
                //this.MidiMsgReceived?.Invoke(this, new MidiMsgEventArgs((int)param1));
                RaiseMidiMsgReceived((int)param1);
                break;
            case WinMMInMsg.MIM_MOREDATA:
                break;
            case WinMMInMsg.MIM_LONGDATA:
                break;
            case WinMMInMsg.MIM_ERROR:
                break;
            case WinMMInMsg.MIM_LONGERROR:
                break;
            }
        }
        
        public override void Close()
        {
            WinMMImport.MidiInReset(this.deviceHandle);
            WinMMImport.MidiInClose(this.deviceHandle);
        }

        public override void Reset()
        {
            WinMMImport.MidiInReset(this.deviceHandle);
        }

        // start Clock
        public override void Start()
        {
            WinMMImport.MidiInStart(this.deviceHandle);
        }

        public override void Stop()
        {
            WinMMImport.MidiInStop(this.deviceHandle);
        }
    }
}
