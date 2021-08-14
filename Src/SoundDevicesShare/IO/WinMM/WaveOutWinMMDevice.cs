using SoundDevices.IO.WinMM.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.WinMM
{
    internal class WaveOutWinMMDevice : WaveOutDevice
    {
        private readonly int deviceID;
        private IntPtr deviceHandle;
        private readonly WinMMImport.Callback deviceCallback;
        private const int bufferNum = 16;
        private const int bufferSize = 1024 * 4;
        private readonly WinMMImport.WAVEHDR[] buffers = new WinMMImport.WAVEHDR[bufferNum];
        private int bufferPut = 0, bufferGet = 0;

        public static void AddDevices(List<WaveOutDevice> devices)
        {
            for (int i = 0; i < WinMMImport.WaveOutGetNumDevs(); i++)
            {
                try
                {
                    devices.Add(new WaveOutWinMMDevice(i));
                }
                catch (SoundDeviceException ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        private WaveOutWinMMDevice(int deviceID)
        {
            this.deviceID = deviceID;
            this.deviceCallback = HandleMessage;

            if (WinMMImport.WaveOutGetDevCaps((IntPtr)deviceID, out WinMMImport.WaveOutCaps waveOutCaps, WinMMImport.WaveOutCapsSize) != 0)
            {
                throw new SoundDeviceException("WaveOutGetDevCaps failed");
            }
            this.DeviceType = SoundDeviceType.WinMM;
            this.Name = waveOutCaps.name;
            this.Version = new Version(waveOutCaps.driverVersion.Major, waveOutCaps.driverVersion.Minor);

            // create buffers
            for (int i = 0; i < bufferNum; i++)
            {
                buffers[i].lpData = Marshal.AllocHGlobal(bufferSize);
                buffers[i].dwBufferLength = bufferSize;
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

                for (int i = 0; i < bufferNum; i++)
                {
                    Marshal.FreeHGlobal(buffers[i].lpData);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        #endregion

        public override void Open(WaveFormat waveFormat = null)
        {
            waveFormat = waveFormat ?? new WaveFormat();

            WinMMImport.WaveFormatEx format = new();
            format.wFormatTag = 1; // WAVE_FORMAT_PCM
            format.nChannels = (short)waveFormat.Channels;
            format.nSamplesPerSec = (short)waveFormat.SampleRate;
            format.wBitsPerSample = (short)waveFormat.BitRate;
            format.nBlockAlign = (short)(format.nChannels * format.wBitsPerSample / 8);
            format.nAvgBytesPerSec = format.nSamplesPerSec * format.nBlockAlign;
            format.cbSize = (short)WinMMImport.WaveFormatExSize;

            WinMMImport.WaveOutOpen(out this.deviceHandle, this.deviceID, format, this.deviceCallback, IntPtr.Zero, WinMMImport.CALLBACK_FUNCTION);
        }

        public override void Reset()
        { }

        public override void Close()
        { }

        public override void Play(Stream stream)
        {
            byte[] buffer = new byte[bufferSize];
            int readSize = stream.Read(buffer, 0, bufferSize);
            Play(buffer, 0, readSize);
        }

        public override void Play(byte[] buffer, int offset, int count)
        {
            Marshal.Copy(buffer, 0, buffers[bufferPut].lpData, bufferSize);
            
            buffers[bufferPut].dwBufferLength = buffer.Length;
            buffers[bufferPut].dwUser = (IntPtr)bufferPut;

            WinMMImport.WaveOutPrepareHeader(this.deviceHandle, buffers[bufferPut], WinMMImport.WaveHeaderSize);
            WinMMImport.WaveOutWrite(this.deviceHandle, buffers[bufferPut], WinMMImport.WaveHeaderSize);
            this.bufferPut = (++this.bufferPut) % bufferNum;
        }

        private void HandleMessage(IntPtr handle, WinMMMsg msg, IntPtr instance, IntPtr param1, IntPtr param2)
        {
            switch (msg)
            {
            case WinMMMsg.WOM_OPEN:
                Debug.WriteLine($"WAVE In device {this.deviceID} {this.Name} open.");
                break;
            case WinMMMsg.WOM_CLOSE:
                Debug.WriteLine($"WAVE In device {this.deviceID} {this.Name} close.");
                break;

            case WinMMMsg.WOM_DONE:
                WinMMImport.WaveOutUnprepareHeader(this.deviceHandle, param1, WinMMImport.WaveHeaderSize);
                //WinMMImport.WAVEHDR waveHeader = Marshal.PtrToStructure<WinMMImport.WAVEHDR>(param1);
                this.bufferGet = (++this.bufferGet) % bufferNum;
                break;
            }
        }
    }
}
