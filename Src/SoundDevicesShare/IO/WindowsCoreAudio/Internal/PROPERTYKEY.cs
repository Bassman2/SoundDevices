using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.WindowsCoreAudio.Internal
{
    public struct PROPERTYKEY
    {
        public Guid fmtid;
        public int pid;

        public PROPERTYKEY(Guid fmtid, int pid)
        {
            this.fmtid = fmtid;
            this.pid = pid;
        }

        public static bool operator ==(PROPERTYKEY pk1, PROPERTYKEY pk2)
        {
            return (pk1.fmtid == pk2.fmtid) && (pk1.pid == pk2.pid);
        }

        public static bool operator !=(PROPERTYKEY pk1, PROPERTYKEY pk2)
        {
            return !(pk1 == pk2);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{fmtid}:{pid}";
        }
    }
}
