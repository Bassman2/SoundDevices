using Microsoft.Win32;
using SoundDevices.IO.ASIO.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Text;

namespace SoundDevices.IO.ASIO
{
    [SupportedOSPlatform("Windows")]
    public class WaveASIODevice : WaveDevice
    {
        private Guid classID;
        private AsioImport asioImport;

        internal static void AddDevices(SoundDeviceType soundDeviceType, List<WaveDevice> devices)
        {
            var regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ASIO");
            if (regKey != null)
            {
                foreach (var name in regKey.GetSubKeyNames())
                {
                    try
                    {
                        var regSubKey = regKey.OpenSubKey(name);
                        var classID = new Guid((string)regSubKey.GetValue("CLSID"));
                        string description = regSubKey.GetValue("Description") as string;
                        int disabled = (int)regSubKey.GetValue("Disabled", 0);
                        if (disabled == 0)
                        {
                            devices.Add(new WaveASIODevice(classID, name, description));
                        }
                    }
                    catch (SoundDeviceException ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }
                regKey.Close();

            }
        }

        private WaveASIODevice(Guid classID, string name, string description)
        {
            this.classID = classID;
            this.DeviceType = SoundDeviceType.ASIO;
            this.Name = name;
            this.Description = description;

            this.asioImport = new AsioImport();
            this.asioImport.InitFromGuid(classID);

            string driverName = this.asioImport.GetDriverName();
            int driverVersion = this.asioImport.GetDriverVersion();

            return;
            this.asioImport.Init(IntPtr.Zero);
            this.asioImport.GetChannels(out int numInputChannels, out int numOutputChannels);

            this.IsInput = numInputChannels > 0;
            for (int i = 0; i < numInputChannels; i++)
            {
                AsioChannelInfo acii = this.asioImport.GetChannelInfo(i, true);
            }
            for (int i = 0; i < numOutputChannels; i++)
            {
                AsioChannelInfo acio = this.asioImport.GetChannelInfo(i, false);
            }
        }

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
        }

        #endregion

        public bool IsInput { get; }

        public override void Open(WaveFormat waveFormat = null)
        { }

        public override void Start()
        { }

        public override void Stop()
        { }

        public override void Reset()
        { }

        public override void Close()
        { }
    }
}
