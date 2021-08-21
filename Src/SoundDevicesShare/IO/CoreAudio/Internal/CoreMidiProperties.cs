using System;

namespace SoundDevices.IO.CoreAudio.Internal
{
    internal static class CoreMidiProperties 
    {
		private const string coreMidiLibrary = "/System/Library/Frameworks/CoreMIDI.framework/CoreMIDI";
		private static readonly IntPtr midiLibrary;

		static CoreMidiProperties()
        {
			midiLibrary = SystemImport.OpenLibrary(coreMidiLibrary);

			AdvanceScheduleTimeMuSec = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyAdvanceScheduleTimeMuSec");
			CanRoute = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyCanRoute");
			ConnectionUniqueID = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyConnectionUniqueID");
			DeviceID = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyDeviceID");
			DisplayName = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyDisplayName");
			DriverDeviceEditorApp = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyDriverDeviceEditorApp");
			DriverOwner = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyDriverOwner");
			DriverVersion = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyDriverVersion");
			Image = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyImage");
			IsBroadcast = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyIsBroadcast");
			IsDrumMachine = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyIsDrumMachine");
			IsEffectUnit = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyIsEffectUnit");
			IsEmbeddedEntity = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyIsEmbeddedEntity");
			IsMixer = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyIsMixer");
			IsSampler = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyIsSampler");
			Manufacturer = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyManufacturer");
			MaxReceiveChannels = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyMaxReceiveChannels");
			MaxSysExSpeed = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyMaxSysExSpeed");
			MaxTransmitChannels = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyMaxTransmitChannels");
			Model = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyModel");
			Name = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyName");
			NameConfiguration = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyNameConfiguration");
			Offline = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyOffline");
			PanDisruptsStereo = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyPanDisruptsStereo");
			Private = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyPrivate");
			ReceiveChannels = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyReceiveChannels");
			ReceivesBankSelectLSB = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyReceivesBankSelectLSB");
			ReceivesBankSelectMSB = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyReceivesBankSelectMSB");
			ReceivesClock = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyReceivesClock");
			ReceivesMTC = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyReceivesMTC");
			ReceivesNotes = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyReceivesNotes");
			ReceivesProgramChanges = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyReceivesProgramChanges");
			SingleRealtimeEntity = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertySingleRealtimeEntity");
			SupportsGeneralMIDI = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertySupportsGeneralMIDI");
			SupportsMMC = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertySupportsMMC");
			SupportsShowControl = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertySupportsShowControl");
			TransmitChannels = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyTransmitChannels");
			TransmitsBankSelectLSB = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyTransmitsBankSelectLSB");
			TransmitsBankSelectMSB = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyTransmitsBankSelectMSB");
			TransmitsClock = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyTransmitsClock");
			TransmitsMTC = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyTransmitsMTC");
			TransmitsNotes = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyTransmitsNotes");
			TransmitsProgramChanges = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyTransmitsProgramChanges");
			UniqueID = SystemImport.GetSymbol(midiLibrary, "kMIDIPropertyUniqueID");

			//SystemImport.CloseLibrary(midiLibrary);
		}

		public static IntPtr AdvanceScheduleTimeMuSec { get; }
		public static IntPtr CanRoute { get; }
		public static IntPtr ConnectionUniqueID { get; }
		public static IntPtr DeviceID { get; }
		public static IntPtr DisplayName { get; }
		public static IntPtr DriverDeviceEditorApp { get; }
		public static IntPtr DriverOwner { get; }
		public static IntPtr DriverVersion { get; }
		public static IntPtr Image { get; }
		public static IntPtr IsBroadcast { get; }
		public static IntPtr IsDrumMachine { get; }
		public static IntPtr IsEffectUnit { get; }
		public static IntPtr IsEmbeddedEntity { get; }
		public static IntPtr IsMixer { get; }
		public static IntPtr IsSampler { get; }
		public static IntPtr Manufacturer { get; }
		public static IntPtr MaxReceiveChannels { get; }
		public static IntPtr MaxSysExSpeed { get; }
		public static IntPtr MaxTransmitChannels { get; }
		public static IntPtr Model { get; }
		public static IntPtr Name { get; }
		public static IntPtr NameConfiguration { get; }
		public static IntPtr Offline { get; }
		public static IntPtr PanDisruptsStereo { get; }
		public static IntPtr Private { get; }
		public static IntPtr ReceiveChannels { get; }
		public static IntPtr ReceivesBankSelectLSB { get; }
		public static IntPtr ReceivesBankSelectMSB { get; }
		public static IntPtr ReceivesClock { get; }
		public static IntPtr ReceivesMTC { get; }
		public static IntPtr ReceivesNotes { get; }
		public static IntPtr ReceivesProgramChanges { get; }
		public static IntPtr SingleRealtimeEntity { get; }
		public static IntPtr SupportsGeneralMIDI { get; }
		public static IntPtr SupportsMMC { get; }
		public static IntPtr SupportsShowControl { get; }
		public static IntPtr TransmitChannels { get; }
		public static IntPtr TransmitsBankSelectLSB { get; }
		public static IntPtr TransmitsBankSelectMSB { get; }
		public static IntPtr TransmitsClock { get; }
		public static IntPtr TransmitsMTC { get; }
		public static IntPtr TransmitsNotes { get; }
		public static IntPtr TransmitsProgramChanges { get; }
		public static IntPtr UniqueID { get; }
	}	
}
