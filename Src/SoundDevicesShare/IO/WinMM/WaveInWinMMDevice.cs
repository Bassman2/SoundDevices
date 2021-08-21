
using SoundDevices.IO.WinMM.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.WinMM
{
    internal class WaveInWinMMDevice : WaveInDevice
    {
        private readonly int deviceID;
        private IntPtr deviceHandle;
        private readonly WinMMImport.Callback deviceCallback;

        internal static void AddDevices(SoundDeviceType soundDeviceType, List<WaveInDevice> devices)
        {
            foreach (var (deviceId, waveInCaps) in WinMMImport.WaveInGetDevices())
            {
                devices.Add(new WaveInWinMMDevice(deviceId, waveInCaps));
            }
        }

        private WaveInWinMMDevice(int deviceID, WinMMImport.WaveInCaps waveInCaps)
        {
            this.deviceID = deviceID;
            this.DeviceType = SoundDeviceType.WinMM;
            this.Name = waveInCaps.name;
            this.Version = new Version(waveInCaps.driverVersion.Major, waveInCaps.driverVersion.Minor);
            this.deviceCallback = OnCallback;
        }

        public override void Dispose()
        {
            if (this.deviceHandle != IntPtr.Zero)
            {
                WinMMImport.WaveInReset(this.deviceHandle);
                WinMMImport.WaveInClose(this.deviceHandle);
                this.deviceHandle = IntPtr.Zero;
            }
        }
        
        private void OnCallback(IntPtr handle, WinMMMsg msg, IntPtr instance, IntPtr param1, IntPtr param2)
        {
            //throw new NotImplementedException();
        }

        public override void Open(WaveFormat waveFormat = null)
        {
            waveFormat = waveFormat ?? new WaveFormat();

            WaveFormatEx format = new();
            format.wFormatTag = 1; // WAVE_FORMAT_PCM
            format.nChannels = (short)waveFormat.Channels;
            format.nSamplesPerSec = (short)waveFormat.SampleRate;
            format.wBitsPerSample = (short)waveFormat.BitRate;
            format.nBlockAlign = (short)(format.nChannels * format.wBitsPerSample / 8);
            format.nAvgBytesPerSec = format.nSamplesPerSec * format.nBlockAlign;
            format.cbSize = (short)WinMMImport.WaveFormatExSize;
            
            WinMMImport.WaveInOpen(out this.deviceHandle, this.deviceID, format, this.deviceCallback, IntPtr.Zero, WinMMImport.CALLBACK_FUNCTION);
        }

        public override void Start()
        {
            WinMMImport.WaveInStart(this.deviceHandle);
        }

        public override void Stop()
        {
            WinMMImport.WaveInStop(this.deviceHandle);

        }

        public override void Reset()
        {
            WinMMImport.WaveInReset(this.deviceHandle);
        }

        public override void Close()
        {
            WinMMImport.WaveInClose(this.deviceHandle);
        }
    }
}
