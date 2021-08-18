using SoundDevices.IO.ALSA.Internal;
using SoundDevices.IO.DirectX.Internal;
using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Text;

namespace SoundDevices.IO.ALSA
{
    [SupportedOSPlatform("Linux")]
    public class MidiInALSADevice : MidiInDevice
    {
        internal static void AddDevices(SoundDeviceType soundDeviceType, List<MidiInDevice> devices)
        {
            foreach (var card in ALSAImport.GetCards())
            {
                int err = SndCtlImport.SndCardGetName(card, out string shortname);
                if (err < 0)
                {
                    throw new SndException("Failed to get shortname of card", err);
                }
                err = SndCtlImport.SndCardGetLongname(card, out string longname);
                if (err < 0)
                {
                    throw new SndException("Failed to get longname of card", err);
                }

                foreach (var (device, ctl) in ALSAImport.GetCardMidiDevices(card))
                {
                    SndCtl sndCtl = ctl;
                    SndrvRawmidiInfo info = new();
                    SndRawmidiImport.SndRawmidiInfoSetSubdevice(ref info, device);

                    SndRawmidiImport.SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Input);
                    SndCtlImport.SndCtlRawmidiInfo(ref sndCtl, ref info);
                    int subs_in = (int)SndRawmidiImport.SndRawmidiInfoGetSubdevicesCount(ref info);
                    SndRawmidiImport.SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Output);

                    SndCtlImport.SndCtlRawmidiInfo(ref sndCtl, ref info);
                    int subs_out = (int)SndRawmidiImport.SndRawmidiInfoGetSubdevicesCount(ref info);
                    int subs = Math.Max(subs_in, subs_out);
                                        
                    bool isOut = ALSAImport.IsOutput(ref sndCtl, card, device, 0);
                    bool isIn = ALSAImport.IsInput(ref sndCtl, card, device, 0);

                    string name = SndRawmidiImport.SndRawmidiInfoGetName(ref info);
                    string sub_name = SndRawmidiImport.SndRawmidiInfoGetSubdeviceName(ref info);

                    if (!string.IsNullOrEmpty(sub_name))
                    {
                        for (int sub = 1; sub < subs; sub++)
                        {
                            isOut = ALSAImport.IsOutput(ref sndCtl, card, device, sub);
                            isIn = ALSAImport.IsInput(ref sndCtl, card, device, sub);

                            SndRawmidiImport.SndRawmidiInfoSetSubdevice(ref info, sub);
                            if (isOut)
                            {
                                SndRawmidiImport.SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Output);
                                err = SndCtlImport.SndCtlRawmidiInfo(ref sndCtl, ref info);
                                if (err < 0)
                                {
                                    throw new SndException($"Failed get rawmidi information {card}:{device}:{sub}", err);
                                }

                            }
                            else
                            {
                                SndRawmidiImport.SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Input);
                                err = SndCtlImport.SndCtlRawmidiInfo(ref sndCtl, ref info);
                                if (err < 0)
                                {
                                    throw new SndException($"Failed get rawmidi information {card}:{device}:{sub}", err);
                                }
                            }
                            sub_name = SndRawmidiImport.SndRawmidiInfoGetSubdeviceName(ref info);
                        }

                    }

                }


            }
        }
        
        internal static void AddDevices2(SoundDeviceType soundDeviceType, List<MidiInDevice> devices)
        {
            int card = -1; // -1 to start the iteration
            int err = SndCtlImport.SndCardNext(ref card);
            if (err < 0)
            {
                throw new SndException("Failed to get number of cards", err);
            }
            while (card >= 0)
            {
                err = SndCtlImport.SndCardGetName(card, out string shortname);
                if (err < 0)
                {
                    throw new SndException("Failed to get shortname of card", err);
                }
                err = SndCtlImport.SndCardGetLongname(card, out string longname);
                if (err < 0)
                {
                    throw new SndException("Failed to get longname of card", err);
                }

                /////////////////////////////////////////////////////////////////////

                SndCtl ctl = new();
                int device = -1;
                string name = $"hw:{card}";
                err = SndCtlImport.SndCtlOpen(ref ctl, ref name, 0);
                if (err < 0)
                {
                    throw new SndException($"Failted to open control for card {card}", err);
                }
                do
                {
                    err = SndCtlImport.SndCtlRawmidiNextDevice(ref ctl, ref device);
                    if (err < 0)
                    {
                        throw new SndException("Failed to determine device number", err);
                    }
                    if (device >= 0)
                    {
                        /////////////////////////////////////////////////////////////////////

                        SndrvRawmidiInfo info = new();
                        SndRawmidiImport.SndRawmidiInfoSetSubdevice(ref info, device);
                        
                        SndRawmidiImport.SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Input);
                        SndCtlImport.SndCtlRawmidiInfo(ref ctl, ref info);
                        int subs_in = (int)SndRawmidiImport.SndRawmidiInfoGetSubdevicesCount(ref info);

                        int sub = 0;
                        SndRawmidiImport.SndRawmidiInfoSetDevice(ref info, (uint)device);
                        SndRawmidiImport.SndRawmidiInfoSetSubdevice(ref info, sub);
                        SndRawmidiImport.SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Input);

                        err = SndCtlImport.SndCtlRawmidiInfo(ref ctl, ref info);
                        bool isOut = err switch
                        {
                            0 => true,
                            -((int)ERRNO.ENXIO) => false,
                            _ => throw new Exception() { HResult = err },
                        };

                        SndRawmidiImport.SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Output);
                        SndCtlImport.SndCtlRawmidiInfo(ref ctl, ref info);
                        int subs_out = (int)SndRawmidiImport.SndRawmidiInfoGetSubdevicesCount(ref info);

                        SndRawmidiImport.SndRawmidiInfoSetDevice(ref info, (uint)device);
                        SndRawmidiImport.SndRawmidiInfoSetSubdevice(ref info, sub);
                        SndRawmidiImport.SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Output);

                        err = SndCtlImport.SndCtlRawmidiInfo(ref ctl, ref info);
                        bool isIn = err switch
                        {
                            0 => true,
                            -((int)ERRNO.ENXIO) => false,
                            _ => throw new Exception() { HResult = err },
                        };

                        name = SndRawmidiImport.SndRawmidiInfoGetName(ref info);
                        string sub_name = SndRawmidiImport.SndRawmidiInfoGetSubdeviceName(ref info);


                        //ListSubdeviceInfo(ref ctl, card, device);

                        /////////////////////////////////////////////////////////////////////
                    }
                } while (device >= 0);
                SndCtlImport.SndCtlClose(ref ctl);


                /////////////////////////////////////////////////////////////////////

                err = SndCtlImport.SndCardNext(ref card);
                if (err < 0)
                {
                    throw new SndException("Failed to get next card number", err);
                }
            }
        }

        public override void Open()
        { }

        public override void Close()
        { }

        public override void Reset()
        { }

        public override void Start()
        { }

        public override void Stop()
        { }
    }
}
