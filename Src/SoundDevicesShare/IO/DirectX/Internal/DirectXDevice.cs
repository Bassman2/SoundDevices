using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace SoundDevices.IO.DirectX.Internal
{
    internal class DirectXDevice
    {

        public delegate bool DSEnumCallback(IntPtr lpGuid, IntPtr lpcstrDescription, IntPtr lpcstrModule, IntPtr lpContext);

        private static bool EnumCallback(IntPtr lpGuid, IntPtr lpcstrDescription, IntPtr lpcstrModule, IntPtr lpContext)
        {
            if (lpGuid != IntPtr.Zero)
            {
                
                byte[] guidBytes = new byte[16];
                Marshal.Copy(lpGuid, guidBytes, 0, 16);
                Guid guid = new Guid(guidBytes);
            
                string description = Marshal.PtrToStringAnsi(lpcstrDescription);
                string moduleName = Marshal.PtrToStringAnsi(lpcstrModule);

                _devices.Add(new WaveOutDirectXDevice { Name = description, DeviceType = SoundDeviceType.DirectX, Description = moduleName });
            }
            return true;
        }

        private static List<WaveOutDevice> _devices;

        public static void GetDevices(List<WaveOutDevice> devices)
        {
            _devices = devices;
            NativeMethods.DirectSoundEnumerate(new DSEnumCallback(EnumCallback), IntPtr.Zero);
        }

        public static class NativeMethods
        {

            [DllImport("dsound.dll", EntryPoint = "DirectSoundEnumerateA", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern void DirectSoundEnumerate(DSEnumCallback lpDSEnumCallback, IntPtr lpContext);
        }
    }
}
