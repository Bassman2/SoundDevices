using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace SoundDevices.IO.DirectX.Internal
{
    internal class DirectXDevice
    {
        private static Guid DSDEVID_DefaultPlayback =new Guid("def00000-9c6d-47ed-aaf1-4dda8f2b5c03");
        private static Guid DSDEVID_DefaultCapture = new Guid("def00001-9c6d-47ed-aaf1-4dda8f2b5c03");


        public delegate bool DSEnumCallback(IntPtr lpGuid, IntPtr lpcstrDescription, IntPtr lpcstrModule, IntPtr lpContext);

        private static bool EnumWaveOutCallback(IntPtr lpGuid, IntPtr lpcstrDescription, IntPtr lpcstrModule, IntPtr lpContext)
        {
            if (lpGuid != IntPtr.Zero)
            {
                byte[] guidBytes = new byte[16];
                Marshal.Copy(lpGuid, guidBytes, 0, 16);
                Guid deviceId = new Guid(guidBytes);
            
                string description = Marshal.PtrToStringAnsi(lpcstrDescription);
                string moduleName = Marshal.PtrToStringAnsi(lpcstrModule);

                waveOutDevices.Add(new WaveOutDirectXDevice(deviceId, description, moduleName));
            }
            else
            {
                NativeMethods.GetDeviceID(ref DSDEVID_DefaultPlayback, out Guid deviceId);
                string description = Marshal.PtrToStringAnsi(lpcstrDescription);
                string moduleName = Marshal.PtrToStringAnsi(lpcstrModule);

                waveOutDevices.Add(new WaveOutDirectXDevice(deviceId, description, moduleName));
            }
            return true;
        }

        private static bool EnumWaveInCallback(IntPtr lpGuid, IntPtr lpcstrDescription, IntPtr lpcstrModule, IntPtr lpContext)
        {
            if (lpGuid != IntPtr.Zero)
            {
                byte[] guidBytes = new byte[16];
                Marshal.Copy(lpGuid, guidBytes, 0, 16);
                Guid deviceId = new Guid(guidBytes);

                string description = Marshal.PtrToStringAnsi(lpcstrDescription);
                string moduleName = Marshal.PtrToStringAnsi(lpcstrModule);

                waveInDevices.Add(new WaveInDirectXDevice(deviceId, description, moduleName));
            }
            else
            {
                NativeMethods.GetDeviceID(ref DSDEVID_DefaultCapture, out Guid deviceId);
                string description = Marshal.PtrToStringAnsi(lpcstrDescription);
                string moduleName = Marshal.PtrToStringAnsi(lpcstrModule);

                waveInDevices.Add(new WaveInDirectXDevice(deviceId, description, moduleName));
            }
            return true;
        }

        private static List<WaveOutDevice> waveOutDevices;
        private static List<WaveInDevice> waveInDevices;

        public static void GetWaveOutDevices(List<WaveOutDevice> devices)
        {
            waveOutDevices = devices;
            NativeMethods.DirectSoundEnumerate(new DSEnumCallback(EnumWaveOutCallback), IntPtr.Zero);
        }

        public static void GetWaveInDevices(List<WaveInDevice> devices)
        {
            waveInDevices = devices;
            NativeMethods.DirectSoundCaptureEnumerate(new DSEnumCallback(EnumWaveInCallback), IntPtr.Zero);
        }

        public static class NativeMethods
        {

            [DllImport("dsound.dll", EntryPoint = "DirectSoundEnumerateA", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern void DirectSoundEnumerate(DSEnumCallback lpDSEnumCallback, IntPtr lpContext);

            [DllImport("dsound.dll", EntryPoint = "DirectSoundCaptureEnumerateA", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern void DirectSoundCaptureEnumerate(DSEnumCallback lpDSEnumCallback, IntPtr lpContext);

            [DllImport("dsound.dll", EntryPoint = "GetDeviceID", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern void GetDeviceID([In]ref Guid guidSrc, out Guid guidDest);
        }
    }
}
