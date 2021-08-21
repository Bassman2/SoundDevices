
using SoundDevices.IO.WinMM.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SoundDevices.IO.WinMM
{
    internal sealed class MidiOutWinMMDevice : MidiOutDevice
    {
        private readonly int deviceID;
        private IntPtr deviceHandle;
        private readonly WinMMImport.Callback deviceCallback;

        internal static void AddDevices(SoundDeviceType soundDeviceType, List<MidiOutDevice> devices)
        {
            foreach (var (deviceId, midiOutCaps) in WinMMImport.MidiOutGetDevices())
            {
                devices.Add(new MidiOutWinMMDevice(deviceId, midiOutCaps));
            }
        }

        private MidiOutWinMMDevice(int deviceID, WinMMImport.MidiOutCaps midiOutCaps)
        {
            this.deviceID = deviceID;
            this.deviceCallback = HandleMessage;
            this.DeviceType = SoundDeviceType.WinMM;
            this.Name = midiOutCaps.name;
            this.Version = new Version(midiOutCaps.driverVersion.Major, midiOutCaps.driverVersion.Minor);
            this.Manufacturer = midiOutCaps.mid.ToString();
        }

        public override void Dispose()
        {
            if (this.deviceHandle != IntPtr.Zero)
            {
                WinMMImport.MidiOutReset(this.deviceHandle);
                WinMMImport.MidiOutClose(this.deviceHandle);
                this.deviceHandle = IntPtr.Zero;
            }
        }

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
