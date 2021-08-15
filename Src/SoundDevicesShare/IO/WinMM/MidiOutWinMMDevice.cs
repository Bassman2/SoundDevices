
using SoundDevices.IO.WinMM.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SoundDevices.IO.WinMM
{
    internal class MidiOutWinMMDevice : MidiOutDevice
    {
        private readonly int deviceID;
        private IntPtr deviceHandle;
        private readonly WinMMImport.Callback deviceCallback;

        internal static void AddDevices(SoundDeviceType soundDeviceType, List<MidiOutDevice> devices)
        {
            // -1 for windows Midi mixer
            for (int i = -1; i < WinMMImport.MidiOutGetNumDevs(); i++)
            {
                try
                {
                    devices.Add(new MidiOutWinMMDevice(i));
                }
                catch (SoundDeviceException ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        private MidiOutWinMMDevice(int deviceID)
        {
            this.deviceID = deviceID;
            this.deviceCallback = HandleMessage;


            if (WinMMImport.MidiOutGetDevCaps((IntPtr)deviceID, out WinMMImport.MidiOutCaps midiOutCaps, WinMMImport.MidiOutCapsSize) != 0)
            {
                throw new SoundDeviceException("MidiOutGetDevCaps failed");
            }

            this.DeviceType = SoundDeviceType.WinMM;
            this.Name = midiOutCaps.name;
            this.Version = new Version(midiOutCaps.driverVersion.Major, midiOutCaps.driverVersion.Minor);
            this.Manufacturer = midiOutCaps.mid.ToString();
        }


        #region IDisposable

        private bool disposedValue;

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    WinMMImport.MidiOutReset(this.deviceHandle);
                    WinMMImport.MidiOutClose(this.deviceHandle);

                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        #endregion

        public override void Open()
        {

            int result = WinMMImport.MidiOutOpen(out this.deviceHandle, this.deviceID, this.deviceCallback, IntPtr.Zero, WinMMImport.CALLBACK_FUNCTION);

        }

        private void HandleMessage(IntPtr hnd, WinMMMsg msg, IntPtr instance, IntPtr param1, IntPtr param2)
        {
            switch (msg)
            {
            case WinMMMsg.MOM_OPEN:
                break;
            case WinMMMsg.MOM_CLOSE:
                break;
            case WinMMMsg.MOM_DONE:
                IntPtr headerPtr = param1;
                WinMMImport.MidiHeader header = new WinMMImport.MidiHeader();
                Marshal.PtrToStructure(headerPtr, header);
                Marshal.FreeHGlobal(header.data);
                Marshal.FreeHGlobal(headerPtr);
                break;            
            }
        }

        public override void Close()
        {
            WinMMImport.MidiOutReset(this.deviceHandle);
            WinMMImport.MidiOutClose(this.deviceHandle);
        }

        public override void Reset()
        {
            WinMMImport.MidiOutReset(this.deviceHandle);
        }

        public override void Send(int msg)
        {
            WinMMImport.MidiOutShortMsg(this.deviceHandle, msg);
        }

        public override void Send(byte[] data)
        {
            WinMMImport.MidiHeader header = new WinMMImport.MidiHeader();
            header.bufferLength = header.bytesRecorded = data.Length;
            header.data = Marshal.AllocHGlobal(data.Length);
            header.flags = 0;

            Marshal.Copy(data, 0, header.data, data.Length);

            IntPtr headerPtr = Marshal.AllocHGlobal(WinMMImport.MidiHeaderSize);
            Marshal.StructureToPtr(header, headerPtr, false);


            int result = WinMMImport.MidiOutPrepareHeader(this.deviceHandle, headerPtr, WinMMImport.MidiHeaderSize);

            result = WinMMImport.MidiOutLongMsg(this.deviceHandle, headerPtr, WinMMImport.MidiHeaderSize);

            if (result != 0)
            {
                WinMMImport.MidiOutUnprepareHeader(this.deviceHandle, headerPtr, WinMMImport.MidiHeaderSize);
            }
        }
    }
}
