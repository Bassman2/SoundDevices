using System;

namespace SoundDevices.IO.CoreMidi.Internal
{
    internal static class MidiProperty 
    {
		private const string coreMidiLibrary = "/System/Library/Frameworks/CoreMIDI.framework/CoreMIDI";
		private static readonly IntPtr midiLibrary;

		static MidiProperty()
        {
			midiLibrary = SystemLibrary.OpenLibrary(coreMidiLibrary);

			AdvanceScheduleTimeMuSec = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyAdvanceScheduleTimeMuSec");
			CanRoute = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyCanRoute");
			ConnectionUniqueID = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyConnectionUniqueID");
			DeviceID = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyDeviceID");
			DisplayName = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyDisplayName");
			DriverDeviceEditorApp = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyDriverDeviceEditorApp");
			DriverOwner = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyDriverOwner");
			DriverVersion = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyDriverVersion");
			Image = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyImage");
			IsBroadcast = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyIsBroadcast");
			IsDrumMachine = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyIsDrumMachine");
			IsEffectUnit = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyIsEffectUnit");
			IsEmbeddedEntity = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyIsEmbeddedEntity");
			IsMixer = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyIsMixer");
			IsSampler = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyIsSampler");
			Manufacturer = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyManufacturer");
			MaxReceiveChannels = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyMaxReceiveChannels");
			MaxSysExSpeed = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyMaxSysExSpeed");
			MaxTransmitChannels = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyMaxTransmitChannels");
			Model = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyModel");
			Name = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyName");
			NameConfiguration = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyNameConfiguration");
			Offline = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyOffline");
			PanDisruptsStereo = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyPanDisruptsStereo");
			Private = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyPrivate");
			ReceiveChannels = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyReceiveChannels");
			ReceivesBankSelectLSB = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyReceivesBankSelectLSB");
			ReceivesBankSelectMSB = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyReceivesBankSelectMSB");
			ReceivesClock = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyReceivesClock");
			ReceivesMTC = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyReceivesMTC");
			ReceivesNotes = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyReceivesNotes");
			ReceivesProgramChanges = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyReceivesProgramChanges");
			SingleRealtimeEntity = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertySingleRealtimeEntity");
			SupportsGeneralMIDI = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertySupportsGeneralMIDI");
			SupportsMMC = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertySupportsMMC");
			SupportsShowControl = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertySupportsShowControl");
			TransmitChannels = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyTransmitChannels");
			TransmitsBankSelectLSB = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyTransmitsBankSelectLSB");
			TransmitsBankSelectMSB = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyTransmitsBankSelectMSB");
			TransmitsClock = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyTransmitsClock");
			TransmitsMTC = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyTransmitsMTC");
			TransmitsNotes = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyTransmitsNotes");
			TransmitsProgramChanges = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyTransmitsProgramChanges");
			UniqueID = SystemLibrary.GetSymbol(midiLibrary, "kMIDIPropertyUniqueID");
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
