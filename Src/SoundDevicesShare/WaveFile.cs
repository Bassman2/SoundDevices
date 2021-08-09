using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SoundDevices
{
    public class WaveFile
    {
        public WaveFile()
        { }

        public void Load(string fileName)
        {
            using FileStream stream = File.Open(fileName, FileMode.Open);
            Load(stream);
        }

        public void Load(Stream stream)
        {
            using BinaryReader reader = new(stream);
        }

        public void Save(string fileName)
        {
            using FileStream stream = File.Create(fileName);
            Save(stream);
        }

        public void Save(Stream stream)
        {
            using BinaryWriter writer = new(stream);
        }
    }
}
