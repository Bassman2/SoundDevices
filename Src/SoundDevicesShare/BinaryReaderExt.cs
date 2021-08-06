using System;
using System.IO;


namespace SoundDevices
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
            Array.Reverse<byte>(array);
            return BitConverter.ToInt16(array, 0);
        }

        public static int ReadBigEndianInt32(this BinaryReader reader)
        {
            byte[] array = reader.ReadBytes(4);
            Array.Reverse<byte>(array);
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
    }
}
