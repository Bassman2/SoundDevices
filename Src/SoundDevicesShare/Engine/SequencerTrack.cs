using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.Engine
{
    public abstract class SequencerTrack
    {
        public SequencerTrack(SequencerTrackType trackType)
        {
            this.TrackType = trackType;
            this.TrackState = SequencerTrackState.Play;
        }

        public SequencerTrackType TrackType { get; }

        public SequencerTrackState TrackState { get; set; }
    }
}
