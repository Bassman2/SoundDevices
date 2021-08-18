using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace SoundDevices.IO.ALSA.Internal
{
    // https://ccrma.stanford.edu/~craig/articles/linuxmidi/alsa-1.0/alsarawportlist.c

    [SupportedOSPlatform("Linux")]
    internal static class ALSAImport
    {
        
        public static void GetCards()
        {
            int status;
            int card = -1;  // use -1 to prime the pump of iterating through card list
            string longname;
            string shortname;

            if ((status = SndCtlImport.SndCardNext(ref card)) < 0)
            {
                Debug.WriteLine("cannot determine card number: " + SndError.SndStrError(status));
                return;
            }
            if (card < 0)
            {
                Debug.WriteLine("no sound cards found");
                return;
            }
            while (card >= 0)
            {
                Debug.WriteLine("Card %d:", card);
                if ((status = SndCtlImport.SndCardGetName(card, out shortname)) < 0)
                {
                    Debug.WriteLine("cannot determine card shortname: %s", SndError.SndStrError(status));
                    break;
                }
                if ((status = SndCtlImport.SndCardGetLongname(card, out longname)) < 0)
                {
                    Debug.WriteLine("cannot determine card longname: " + SndError.SndStrError(status));
                    break;
                }
                Debug.WriteLine($"\tLONG NAME:  {longname}\n");
                Debug.WriteLine($"\tSHORT NAME: {shortname}\n");
                if ((status = SndCtlImport.SndCardNext(ref card)) < 0)
                {
                    Debug.WriteLine("cannot determine card number: " + SndError.SndStrError(status));
                    break;
                }
            }

        }

        public static void GetMidiPorts()
        {
            int status;
            int card = -1;  // use -1 to prime the pump of iterating through card list

            if ((status = SndCtlImport.SndCardNext(ref card)) < 0)
            {
                Debug.WriteLine("cannot determine card number: %s", SndError.SndStrError(status));
                return;
            }
            if (card < 0)
            {
                Debug.WriteLine("no sound cards found");
                return;
            }
            Debug.WriteLine("\nDir Device    Name\n");
            Debug.WriteLine("====================================\n");
            while (card >= 0)
            {
                list_midi_devices_on_card(card);
                if ((status = SndCtlImport.SndCardNext(ref card)) < 0)
                {
                    Debug.WriteLine("cannot determine card number: %s", SndError.SndStrError(status));
                    break;
                }
            }
            Debug.WriteLine("\n");
        }

        private static void list_midi_devices_on_card(int card)
        {
            SndCtl ctl = new();
            string name;
            int device = -1;
            int status;
            name = $"hw:{card}";
            if ((status = SndCtlImport.SndCtlOpen(ref ctl, ref name, 0)) < 0)
            {
                Debug.WriteLine("cannot open control for card %d: %s", card, SndError.SndStrError(status));
                return;
            }
            do
            {
                status = SndCtlImport.SndCtlRawmidiNextDevice(ref ctl, ref device);
                if (status < 0)
                {
                    Debug.WriteLine("cannot determine device number: %s", SndError.SndStrError(status));
                    break;
                }
                if (device >= 0)
                {
                    list_subdevice_info(ref ctl, card, device);
                }
            } while (device >= 0);
            SndCtlImport.SndCtlClose(ref ctl);
        }

        static void list_subdevice_info(ref SndCtl ctl, int card, int device)
        {
            SndrvRawmidiInfo info = new();
            string name;
            string sub_name;
            int subs, subs_in, subs_out;
            int sub;
            bool isIn, isOut;
            int status;

            //snd_rawmidi_info_alloca(&info);
            SndRawmidiImport.SndRawmidiInfoSetSubdevice(ref info, device);

            SndRawmidiImport.SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Input);
            SndCtlImport.SndCtlRawmidiInfo(ref ctl, ref info);
            subs_in = (int)SndRawmidiImport.SndRawmidiInfoGetSubdevicesCount(ref info);
            SndRawmidiImport.SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Output);
            SndCtlImport.SndCtlRawmidiInfo(ref ctl, ref info);
            subs_out = (int)SndRawmidiImport.SndRawmidiInfoGetSubdevicesCount(ref info);
            subs = subs_in > subs_out ? subs_in : subs_out;

            sub = 0;
            isIn = isOut = false;
            try
            {
                isOut = is_output(ref ctl, card, device, sub);
                isIn = is_input(ref ctl, card, device, sub);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("cannot get rawmidi information %d:%d: %s", card, device, SndError.SndStrError(ex.HResult));
                return;
            }

            name = SndRawmidiImport.SndRawmidiInfoGetName(ref info);
            sub_name = SndRawmidiImport.SndRawmidiInfoGetSubdeviceName(ref info);
            if (sub_name[0] == '\0')
            {
                if (subs == 1)
                {
                    Debug.WriteLine("%c%c  hw:%d,%d    %s\n", isIn ? 'I' : ' ', isOut ? 'O' : ' ', card, device, name);
                }
                else
                {
                    Debug.WriteLine("%c%c  hw:%d,%d    %s (%d subdevices)\n", isIn ? 'I' : ' ', isOut ? 'O' : ' ', card, device, name, subs);
                }
            }
            else
            {
                sub = 0;
                for (; ; )
                {
                    Debug.WriteLine("%c%c  hw:%d,%d,%d  %s\n", isIn ? 'I' : ' ', isOut ? 'O' : ' ', card, device, sub, sub_name);
                    if (++sub >= subs)
                        break;

                    isIn = is_input(ref ctl, card, device, sub);
                    isOut = is_output(ref ctl, card, device, sub);
                    SndRawmidiImport.SndRawmidiInfoSetSubdevice(ref info, sub);
                    if (isOut)
                    {
                        SndRawmidiImport.SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Output);
                        if ((status = SndCtlImport.SndCtlRawmidiInfo(ref ctl, ref info)) < 0)
                        {
                            Debug.WriteLine("cannot get rawmidi information %d:%d:%d: %s", card, device, sub, SndError.SndStrError(status));
                            break;
                        }

                    }
                    else
                    {
                        SndRawmidiImport.SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Input);
                        if ((status = SndCtlImport.SndCtlRawmidiInfo(ref ctl, ref info)) < 0)
                        {
                            Debug.WriteLine("cannot get rawmidi information %d:%d:%d: %s", card, device, sub, SndError.SndStrError(status));
                            break;
                        }
                    }
                    sub_name = SndRawmidiImport.SndRawmidiInfoGetSubdeviceName(ref info);
                }
            }
        }




        //////////////////////////////
        //
        // is_input -- returns true if specified card/device/sub can output MIDI data.
        //

        public static bool is_input(ref SndCtl ctl, int card, int device, int sub)
        {
            SndrvRawmidiInfo info = new();

            SndRawmidiImport.SndRawmidiInfoSetDevice(ref info, (uint)device);
            SndRawmidiImport.SndRawmidiInfoSetSubdevice(ref info, sub);
            SndRawmidiImport.SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Input);

            int status = SndCtlImport.SndCtlRawmidiInfo(ref ctl, ref info);
            return status switch
            {
                0 => true,
                -((int)ERRNO.ENXIO) => false,
                _ => throw new Exception() { HResult = status },
            };
        }



        //////////////////////////////
        //
        // is_output -- returns true if specified card/device/sub can output MIDI data.
        //

        public static bool is_output(ref SndCtl ctl, int card, int device, int sub)
        {
            SndrvRawmidiInfo info = new();

            SndRawmidiImport.SndRawmidiInfoSetDevice(ref info, (uint)device);
            SndRawmidiImport.SndRawmidiInfoSetSubdevice(ref info, sub);
            SndRawmidiImport.SndRawmidiInfoSetStream(ref info, SndRawmidiStream.Output);

            int status = SndCtlImport.SndCtlRawmidiInfo(ref ctl, ref info);
            return status switch
            {
                0 => true,
                -((int)ERRNO.ENXIO) => false,
                _ => throw new Exception() { HResult = status },
            };
        }

        
       

    }
}
