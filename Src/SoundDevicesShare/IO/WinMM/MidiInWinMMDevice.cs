using SoundDevices.IO.WinMM.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SoundDevices.IO.WinMM
{
    internal sealed class MidiInWinMMDevice : MidiInDevice
    {
        private readonly int deviceID;
        private IntPtr deviceHandle;
        private readonly WinMMImport.Callback deviceCallback;

        internal static void AddDevices(SoundDeviceType soundDeviceType, List<MidiInDevice> devices)
        {
            foreach (var (deviceId, midiInCaps) in WinMMImport.MidiInGetDevices())
            {
                devices.Add(new MidiInWinMMDevice(deviceId, midiInCaps));
            }
        }

        private MidiInWinMMDevice(int deviceID, WinMMImport.MidiInCaps midiInCaps)
        {
            this.deviceID = deviceID;
            this.deviceCallback = HandleMessage;
            this.DeviceType = SoundDeviceType.WinMM;
            this.Name = midiInCaps.name;
            this.Version = new Version(midiInCaps.driverVersion.Major, midiInCaps.driverVersion.Minor);
            this.Manufacturer = midiInCaps.mid.ToString();
        }

        public override void Dispose()
        {
            if (this.deviceHandle != IntPtr.Zero)
            {
                WinMMImport.MidiInReset(this.deviceHandle);
                WinMMImport.MidiInClose(this.deviceHandle);
                this.deviceHandle = IntPtr.Zero;
            }
        }

        public override void Open()
        {
            int result = WinMMImport.MidiInOpen(out this.deviceHandle, this.deviceID, this.deviceCallback, IntPtr.Zero, WinMMImport.CALLBACK_FUNCTION);
        }

        public override void Start()
        {
            WinMMImport.MidiInStart(this.deviceHandle);
        }

        public override void Stop()
        {
            WinMMImport.MidiInStop(this.deviceHandle);
        }
        
        public override void Reset()
        {
            WinMMImport.MidiInReset(this.deviceHandle);
        }

        public override void Close()
        {
            WinMMImport.MidiInReset(this.deviceHandle);
            WinMMImport.MidiInClose(this.deviceHandle);
        }

        // start Clock


        private void HandleMessage(IntPtr hnd, WinMMMsg msg, IntPtr instance, IntPtr param1, IntPtr param2)
        {
            switch (msg)
            {
            case WinMMMsg.MIM_OPEN:
                break;
            case WinMMMsg.MIM_CLOSE:
                break;
            case WinMMMsg.MIM_DATA:
                //this.MidiMsgReceived?.Invoke(this, new MidiMsgEventArgs((int)param1));
                RaiseMidiMsgReceived((int)param1);
                break;
            case WinMMMsg.MIM_MOREDATA:
                break;
            case WinMMMsg.MIM_LONGDATA:
                break;
            case WinMMMsg.MIM_ERROR:
                break;
            case WinMMMsg.MIM_LONGERROR:
                break;
            }
        }
    }
}
