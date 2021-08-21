using System;

namespace SoundDevices.IO.CoreAudio.Internal
{
    [Flags]
	internal enum MidiObjectType
	{
		Other = -1,
		Device, Entity, Source, Destination,
		ExternalMask = 0x10,
		ExternalDevice = ExternalMask | Device,
		ExternalEntity = ExternalMask | Entity,
		ExternalSource = ExternalMask | Source,
		ExternalDestination = ExternalMask | Destination,
	}
}
