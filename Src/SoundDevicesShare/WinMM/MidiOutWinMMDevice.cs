
using SoundDevices.WinMM.Internal;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SoundDevices.WinMM
{
    internal class MidiOutWinMMDevice : MidiOutDevice
    {
        private readonly int deviceID;
        private IntPtr deviceHandle;
        private readonly WinMMImport.MidiOutProc midiOutProc;

        internal static void AddDevices(List<MidiOutDevice> devices)
        {
            for (int i = 0; i < WinMMImport.MidiOutGetNumDevs(); i++)
            {
                if (WinMMImport.MidiOutGetDevCaps((IntPtr)i, out WinMMImport.MidiOutCaps midiOutCaps, WinMMImport.MidiOutCapsSize) == 0)
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

        private MidiOutWinMMDevice(int deviceID)
        {
            this.deviceID = deviceID;
            this.midiOutProc = HandleMessage;


            if (WinMMImport.MidiOutGetDevCaps((IntPtr)deviceID, out WinMMImport.MidiOutCaps midiOutCaps, WinMMImport.MidiOutCapsSize) != 0)
            {
                throw new SoundDeviceException("MidiOutGetDevCaps failed");
            }

            this.DeviceType = SoundDeviceType.WinMM;
            this.Name = midiOutCaps.name;
            this.Version = new Version(midiOutCaps.driverVersion.Major, midiOutCaps.driverVersion.Minor);
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

        public void Open()
        {

            int result = WinMMImport.MidiOutOpen(out this.deviceHandle, this.deviceID, this.midiOutProc, IntPtr.Zero, WinMMImport.CALLBACK_FUNCTION);

        }

        private void HandleMessage(IntPtr hnd, int msg, IntPtr instance, IntPtr param1, IntPtr param2)
        {
            switch ((WinMMOutMsg)msg)
            {
            case WinMMOutMsg.MOM_OPEN:
                break;
            case WinMMOutMsg.MOM_CLOSE:
                break;
            case WinMMOutMsg.MOM_DONE:
                IntPtr headerPtr = param1;
                WinMMImport.MidiHeader header = new WinMMImport.MidiHeader();
                Marshal.PtrToStructure(headerPtr, header);
                Marshal.FreeHGlobal(header.data);
                Marshal.FreeHGlobal(headerPtr);
                break;            
            }
        }

        public void Close()
        {
            WinMMImport.MidiOutReset(this.deviceHandle);
            WinMMImport.MidiOutClose(this.deviceHandle);
        }

        public void Reset()
        {
            WinMMImport.MidiOutReset(this.deviceHandle);
        }

        public void Send(int msg)
        {
            WinMMImport.MidiOutShortMsg(this.deviceHandle, msg);
        }

        public void Send(byte[] data)
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
