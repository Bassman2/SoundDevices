using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SoundDevices.IO
{
    internal static class BinaryReaderExt
    {
        public static string ReadChunkID(this BinaryReader reader)
        {
            return new string(reader.ReadChars(4));
        }

        public static int ReadBigEndianInt16(this BinaryReader reader)
        {
            byte[] array = reader.ReadBytes(2);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse<byte>(array);
            }
            return BitConverter.ToInt16(array, 0);
        }

        public static int ReadBigEndianInt24(this BinaryReader reader)
        {
            byte[] array = reader.ReadBytes(3);
            Array.Resize(ref array, 4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse<byte>(array);
            }
            return BitConverter.ToInt32(array, 0);
        }

        public static int ReadBigEndianInt32(this BinaryReader reader)
        {
            byte[] array = reader.ReadBytes(4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse<byte>(array);
            }
            return BitConverter.ToInt32(array, 0);
        }

        public static void CopyToStream(this BinaryReader reader, Stream stream, int size)
        {
            const int  bufferSize = 32768;
            byte[] buffer = new byte[bufferSize];
            int read;
            while (size > 0 && (read = reader.Read(buffer, 0, Math.Min(size, bufferSize))) > 0)
            { 
                stream.Write(buffer, 0, read);
                size -= read;
            }
        }

        public static long ReadMidiTime(this BinaryReader reader)
        {
            long time = 0;
            byte b;
            while ((b = reader.ReadByte()) > 0x80 )
            {
                time += b & 0x7F;
                time <<= 7;
            }
            time += b;
            return time;
        }

        public static string ReadMidiText(this BinaryReader reader)
        {
            int len = reader.ReadByte();
            byte[] text = reader.ReadBytes(len);
            return Encoding.ASCII.GetString(text);
        }

        public static byte[] ReadMidiSysEx(this BinaryReader reader)
        {
            byte b;
            List<byte> arr = new(256);
            while ((b = reader.ReadByte()) != 0xF7)
            {
                arr.Add(b);
            }
            return arr.ToArray();   
        }

        public static short ReadMidiInt16(this BinaryReader reader)
        {
            return (short)(((short)reader.ReadByte()) << 7 + reader.ReadByte());
        }
    }
}
