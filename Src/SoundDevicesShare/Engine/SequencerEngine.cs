using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.Engine
{
    public sealed class SequencerEngine : IDisposable
    {
        public SequencerEngine()
        {
            this.Tracks = new();
        }

        #region IDisposable

        public void Dispose()
        {
            
            //GC.SuppressFinalize(this);
        }

        #endregion

        public void Init()
        { }

        public void Play()
        { }

        public void Record()
        { }
        
        public void Stop()
        { }

        public List<SequencerTrack> Tracks { get; }

        public long Position { get; set;}
        
    }
}
