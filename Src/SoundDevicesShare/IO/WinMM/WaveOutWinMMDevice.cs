using SoundDevices.IO.WinMM.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.WinMM
{
    internal sealed class WaveOutWinMMDevice : WaveOutDevice
    {
        private readonly int deviceID;
        private IntPtr deviceHandle;
        private readonly WinMMImport.Callback deviceCallback;
        private const int bufferNum = 16;
        private const int bufferSize = 1024 * 4;
        private readonly WinMMImport.WAVEHDR[] buffers = new WinMMImport.WAVEHDR[bufferNum];
        private int bufferPut = 0, bufferGet = 0;

        public static void AddDevices(SoundDeviceType soundDeviceType, List<WaveOutDevice> devices)
        {
            foreach (var (deviceId, waveOutCaps) in WinMMImport.WaveOutGetDevices())
            {
                devices.Add(new WaveOutWinMMDevice(deviceId, waveOutCaps));
            }
        }

        private WaveOutWinMMDevice(int deviceID, WinMMImport.WaveOutCaps waveOutCaps)
        {
            this.deviceID = deviceID;
            this.deviceCallback = HandleMessage;
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

        public override void Dispose()
        {
            if (this.deviceHandle != IntPtr.Zero)
            {
                WinMMImport.WaveOutReset(this.deviceHandle);
                WinMMImport.WaveOutClose(this.deviceHandle);
                this.deviceHandle = IntPtr.Zero;

                //for (int i = 0; i < bufferNum; i++)
                //{
                //    Marshal.FreeHGlobal(buffers[i].lpData);
                //}
            }
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
