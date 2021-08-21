using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace SoundDevices.IO.CoreAudio.Internal
{
    // https://developer.apple.com/documentation/coremidi

    // https://developer.apple.com/documentation/coremidi/1495164-midigetnumberofdevices

    // https://apisof.net/catalog/e61ee9a2-d72e-f028-238a-5f135f9bc063

    // https://github.com/kjpou1/maccore

    // https://apisof.net/catalog/e61ee9a2-d72e-f028-238a-5f135f9bc063

    [SupportedOSPlatform("MacOS")]
    internal static class CoreMidiImport 
    {
        public const string CoreMidiLibrary = "/System/Library/Frameworks/CoreMIDI.framework/CoreMIDI";
        public const string SystemLibrary = "/usr/lib/libSystem.dylib";

        //private static IntPtr coreMidiHandle;

        //static CoreMidiImport()
        //{
        //    //coreMidiHandle = dlopen(CoreMidiLibrary, 0);
        //}

        /*
        internal static CoreMidiImport()
        {
            Console.WriteLine("Hello World!");

            var midiLibrary = dlopen(CoreMidiLibrary, 0);


            var indirect = dlsym(midiLibrary, "kMIDIPropertyDisplayName");
            if (indirect == IntPtr.Zero)
                return;
            IntPtr displayName = Marshal.ReadIntPtr(indirect);





            //var indirect = dlsym(handle, symbol);
            //if (indirect == IntPtr.Zero)
            //    return IntPtr.Zero;
            //return Marshal.ReadIntPtr(indirect);
            //kMIDIPropertyDisplayName = Dlfcn.GetIntPtr(midiLibrary, "kMIDIPropertyDisplayName");
            //dlsym(IntPtr handle, string symbol);

            int devNum = MIDIGetNumberOfDevices();
            Console.WriteLine($"MIDIGetNumberOfDevices: {devNum}");

            for (int i = 0; i < devNum; i++)
            {
                IntPtr ptr = MIDIGetDevice(i);
            }

            int extDevNum = MIDIGetNumberOfExternalDevices();
            Console.WriteLine($"MIDIGetNumberOfExternalDevices: {extDevNum}");

            
            int desNum = MIDIGetNumberOfDestinations();
            Console.WriteLine($"MIDIGetNumberOfDestinations: {desNum}");

            int srcNum = MIDIGetNumberOfSources();
            Console.WriteLine($"MIDIGetNumberOfSources: {srcNum}");
        }
        */

        //public static MidiDevice GetDevice(int deviceIndex)
        //{
        //    var h = MIDIGetDevice(deviceIndex);
        //    if (h == IntPtr.Zero)
        //        return null;
        //    return new MidiDevice(h);
        //}

        //public static MidiDevice GetExternalDevice(int deviceIndex)
        //{
        //    var h = MIDIGetExternalDevice(deviceIndex);
        //    if (h == IntPtr.Zero)
        //        return null;
        //    return new MidiDevice(h);
        //}

        //public static string MIDIObjectGetStringProperty(IntPtr handle)
        //{
        //    IntPtr val;
        //    int code;

        //    //kMIDIPropertyDisplayName = Dlfcn.GetIntPtr(midiLibrary, "kMIDIPropertyDisplayName");

        //    code = MIDIObjectGetStringProperty(handle, MidiProperty.DisplayName, out val);
        //    if (code == 0)
        //    {
        //        string str = Marshal.PtrToStringAnsi(val);
        //        //Marshal.Release(val);

        //        return str;
        //    }
        //    return null;
        //}

        #region Properties

        public static string MIDIObjectGetStringProperty(IntPtr deviceHandle, IntPtr symbol)
        {
            int code = MIDIObjectGetStringProperty(deviceHandle, symbol, out IntPtr data);
            if (code == 0)
            {
                return Marshal.PtrToStringAnsi(data);
            }
            return null;
        }

        public static int MIDIObjectGetIntegerProperty(IntPtr deviceHandle, IntPtr symbol)
        {
            int code = MIDIObjectGetIntegerProperty(deviceHandle, symbol, out int data);
            if (code == 0)
            {
                return data;
            }
            return 0;
        }

        public static string MidiObjectGetDeviceID(IntPtr deviceHandle) => MIDIObjectGetStringProperty(deviceHandle, CoreMidiProperties.DeviceID);
        public static string MidiObjectGetDisplayName(IntPtr deviceHandle) => MIDIObjectGetStringProperty(deviceHandle, CoreMidiProperties.DisplayName);
        public static string MidiObjectGetManufacturer(IntPtr deviceHandle) => MIDIObjectGetStringProperty(deviceHandle, CoreMidiProperties.Manufacturer);
        public static string MidiObjectGetModel(IntPtr deviceHandle) => MIDIObjectGetStringProperty(deviceHandle, CoreMidiProperties.Model);
        public static string MidiObjectGetName(IntPtr deviceHandle) => MIDIObjectGetStringProperty(deviceHandle, CoreMidiProperties.Name);
        public static string MidiObjectGetNameConfiguration(IntPtr deviceHandle) => MIDIObjectGetStringProperty(deviceHandle, CoreMidiProperties.NameConfiguration);
        public static string MidiObjectGetUniqueID(IntPtr deviceHandle) => MIDIObjectGetStringProperty(deviceHandle, CoreMidiProperties.UniqueID);
        public static int MidiObjectGetDriverVersion(IntPtr deviceHandle) => MIDIObjectGetIntegerProperty(deviceHandle, CoreMidiProperties.DriverVersion);
        #endregion

        public delegate void MidiNotifyProc(IntPtr message, IntPtr context);
        public delegate void MidiReadProc(IntPtr packetList, IntPtr context, IntPtr srcPtr);



        [DllImport(CoreMidiLibrary)]
        public extern static void MIDIRestart();

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIGetNumberOfDevices();

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIGetNumberOfExternalDevices();

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIGetNumberOfDestinations();

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIGetNumberOfSources();

        [DllImport(CoreMidiLibrary)]
        public extern static IntPtr MIDIGetExternalDevice(int item);

        [DllImport(CoreMidiLibrary)]
        public extern static IntPtr MIDIGetDevice(int item);
                
        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIObjectGetIntegerProperty(IntPtr obj, IntPtr str, out int ret);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIObjectSetIntegerProperty(IntPtr obj, IntPtr str, int val);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIObjectGetDictionaryProperty(IntPtr obj, IntPtr str, out IntPtr dict);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIObjectSetDictionaryProperty(IntPtr obj, IntPtr str, IntPtr dict);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIObjectGetDataProperty(IntPtr obj, IntPtr str, out IntPtr data);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIObjectSetDataProperty(IntPtr obj, IntPtr str, IntPtr data);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIObjectGetStringProperty(IntPtr obj, IntPtr str, out IntPtr data);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIObjectSetStringProperty(IntPtr obj, IntPtr str, IntPtr nstr);

        [DllImport(CoreMidiLibrary)]
        public extern static MidiError MIDIObjectRemoveProperty(IntPtr obj, IntPtr str);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIObjectGetProperties(IntPtr obj, out IntPtr dict, bool deep);

        [DllImport(CoreMidiLibrary)]
        public extern static MidiError MIDIObjectFindByUniqueID(int uniqueId, out IntPtr obj, out MidiObjectType objectType);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIClientCreate(IntPtr str, MidiNotifyProc callback, IntPtr context, out IntPtr handle);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIClientDispose(IntPtr handle);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDISourceCreate(IntPtr handle, IntPtr name, out IntPtr endpoint);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIInputPortCreate(IntPtr client, IntPtr portName, MidiReadProc readProc, IntPtr context, out IntPtr midiPort);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIOutputPortCreate(IntPtr client, IntPtr portName, out IntPtr midiPort);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIPortDispose(IntPtr port);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIPortConnectSource(IntPtr port, IntPtr endpoint, IntPtr context);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIPortDisconnectSource(IntPtr port, IntPtr endpoint);

        [DllImport(CoreMidiLibrary)]
        public extern static MidiError MIDISend(IntPtr port, IntPtr endpoint, IntPtr packets);

        [DllImport(CoreMidiLibrary)]
        public extern static IntPtr MIDIEntityGetDestination(IntPtr entity, int idx);

        [DllImport(CoreMidiLibrary)]
        public extern static IntPtr MIDIEntityGetSource(IntPtr entity, int idx);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIEntityGetNumberOfDestinations(IntPtr entity);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIEntityGetNumberOfSources(IntPtr entity);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIEntityGetDevice(IntPtr handle, out IntPtr devRef);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIDeviceGetNumberOfEntities(IntPtr handle);

        [DllImport(CoreMidiLibrary)]
        public extern static IntPtr MIDIDeviceGetEntity(IntPtr handle, int item);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIEndpointDispose(IntPtr handle);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIDestinationCreate(IntPtr client, IntPtr name, MidiReadProc readProc, IntPtr context, out IntPtr midiEndpoint);

        [DllImport(CoreMidiLibrary)]
        public extern static int MIDIFlushOutput(IntPtr handle);

        [DllImport(CoreMidiLibrary)]
        public extern static MidiError MIDIReceived(IntPtr handle, IntPtr packetList);

        [DllImport(CoreMidiLibrary)]
        public extern static IntPtr MIDIGetSource(int sourceIndex);

        [DllImport(CoreMidiLibrary)]
        public extern static IntPtr MIDIGetDestination(int destinationIndex);


    }

    /*
    public class CoreMidiDevice
    {
        public const string CoreMidiLibrary = "/System/Library/Frameworks/CoreMIDI.framework/CoreMIDI";

        [DllImport(CoreMidiLibrary)]
        private extern static void MIDIRestart();

        [DllImport(CoreMidiLibrary)]
        private extern static int MIDIGetNumberOfExternalDevices();


		[DllImport(CoreMidiLibrary)]
		private extern static int MIDIGetNumberOfDestinations();
		[DllImport(CoreMidiLibrary)]
		private extern static int MIDIGetNumberOfSources();
				
		[DllImport(CoreMidiLibrary)]
		private extern static int MIDIGetNumberOfDevices();

		public static int ExternalDeviceCount
		{
			get
			{
				return MIDIGetNumberOfExternalDevices();
			}
		}

		public static int DeviceCount
		{
			get
			{
				return MIDIGetNumberOfDevices();
			}
		}

		[DllImport(CoreMidiLibrary)]
		private extern static IntPtr MIDIGetExternalDevice(int item);

		[DllImport(CoreMidiLibrary)]
		private extern static IntPtr MIDIGetDevice(int item);
	}  
      
    */
}
