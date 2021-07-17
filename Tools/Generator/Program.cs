using System;
using System.CodeDom.Compiler;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new();
            parser.Parse(@"C:\Program Files (x86)\Windows Kits\10\Include\10.0.17134.0\um\dmusicc.h");
            //parser.Parse(@"C:\Program Files (x86)\Windows Kits\10\Include\10.0.19041.0\um\dmusics.h");
            //parser.Parse(@"C:\Program Files (x86)\Windows Kits\10\Include\10.0.17134.0\um\dsound.h");
            //parser.Parse(@"C:\Program Files (x86)\Windows Kits\10\Include\10.0.19041.0\shared\dmusbuff.h");

            Generator generator = new();
            generator.Generate(parser, @"C:\Projects\SoundDevices\Src\MediaDevicesShare\IO\Internal\DirectMusic\COMInterface\", "DirectMusic", "MediaDevices.IO.Internal.DirectMusic.COMInterface");
        }
    }
}
