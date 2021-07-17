using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class Generator
    {
        public Generator()
        { }

        public void Generate(Parser parser, string path, string name, string nameSpace)
        {
            path = Path.GetFullPath(path);
            Directory.CreateDirectory(path);
            string file = Path.Combine(path, name + ".cs");

            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.WriteLine("using System;");
                sw.WriteLine("using System.Runtime.InteropServices;");
                sw.WriteLine();
                sw.WriteLine($"namespace {nameSpace}");
                sw.WriteLine("{");

                sw.WriteLine("}");
            }

            foreach (var val in parser.Interfaces.Values)
            {
                file = Path.Combine(path, val.Name + ".cs");

                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.WriteLine("using System;");
                    sw.WriteLine("using System.Runtime.InteropServices;");
                    sw.WriteLine();
                    sw.WriteLine($"namespace {nameSpace}");
                    sw.WriteLine("{");
                    sw.WriteLine("    [ComImport]");
                    sw.WriteLine("    [Guid(\"d2ac2878-b39b-11d1-8704-00600893b1bd\")]");
                    sw.WriteLine("    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]");
                    sw.WriteLine($"    internal interface {val.Name}");
                    sw.WriteLine("    {");
                    foreach (var v in val.Methods)
                    {
                        if (v.Params.Count == 0)
                        {
                            sw.WriteLine($"        void {v.Name}();");
                        }
                        else
                        {
                            sw.WriteLine($"        void {v.Name}(");

                            foreach (var p in v.Params.SkipLast(1))
                            {
                                if (!string.IsNullOrEmpty(p.Attr))
                                {
                                    sw.WriteLine($"            {p.Attr}");
                                }
                                sw.WriteLine($"            {p.Type} {p.Name},");
                            }
                            var ep = v.Params.Last();
                            if (!string.IsNullOrEmpty(ep.Attr))
                            {
                                sw.WriteLine($"            {ep.Attr}");
                            }
                            sw.WriteLine($"            {ep.Type} {ep.Name});");
                        }
                        sw.WriteLine();
                    }
                    sw.WriteLine("    }");
                    sw.WriteLine("}");
                }
            }

            foreach (var val in parser.Structs.Values)
            {
                file = Path.Combine(path, val.Name + ".cs");

                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.WriteLine("using System;");
                    sw.WriteLine("using System.Runtime.InteropServices;");
                    sw.WriteLine();
                    sw.WriteLine($"namespace {nameSpace}");
                    sw.WriteLine("{");
                    sw.WriteLine("    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]");
                    sw.WriteLine($"    internal struct {val.Name}");
                    sw.WriteLine("    {");
                    foreach (var v in val.Params)
                    {
                        if (!string.IsNullOrEmpty(v.Attr))
                        {
                            sw.WriteLine($"        {v.Attr}");
                        }
                        sw.WriteLine($"        {v.Type} {v.Name};");
                    }
                    sw.WriteLine("    }");
                    sw.WriteLine("}");
                }
            }

            foreach (var val in parser.Enums.Values)
            {
                file = Path.Combine(path, val.Name + ".cs");

                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.WriteLine("using System;");
                    sw.WriteLine("using System.Runtime.InteropServices;");
                    sw.WriteLine();
                    sw.WriteLine($"namespace {nameSpace}");
                    sw.WriteLine("{");
                    sw.WriteLine($"    internal enum {val.Name}");
                    sw.WriteLine("    {");
                    foreach (var v in val.Vars)
                    {
                        sw.WriteLine($"        {v.Name} = {v.Val},");
                    }
                    sw.WriteLine("    }");
                    sw.WriteLine("}");
                }
            }
        }
    }
}
