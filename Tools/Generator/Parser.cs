using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Generator
{
    public class Parser
    {
        private ParserState parserState = ParserState.NoState;

        private const string guidreg = @"0x(?<a>[a-fA-F0-9)]+),\s*0x(?<b>[a-fA-F0-9)]+),\s*0x(?<c>[a-fA-F0-9)]+),\s*0x(?<d>[a-fA-F0-9)]+),\s*0x(?<e>[a-fA-F0-9)]+),\s*0x(?<f>[a-fA-F0-9)]+),\s*0x(?<g>[a-fA-F0-9)]+),\s*0x(?<h>[a-fA-F0-9)]+),\s*0x(?<i>[a-fA-F0-9)]+),\s*0x(?<j>[a-fA-F0-9)]+),\s*0x(?<k>[a-fA-F0-9)]+)";
        private const string namereg = @"(?<func>[A-Z0-9_]+)";
        private const string parareg = @"(?<type>[A-Z0-9_]+)\s*(?<ptr>[\*]*)\s*(?<name>[A-Z0-9_]+)";

        public class InterfaceData
        {
            public InterfaceData()
            {
                this.Methods = new();
            }
            public string Name { get; set; }
            public List<MethodData> Methods { get; }
        }
        
        public class MethodData
        {
            public MethodData()
            {
                this.Params = new();
            }

            public string Name { get; set; }
            public List<Parameter> Params { get; }
        }

        public class StructData
        {
            public StructData()
            {
                this.Params = new();
            }
            public string Name { get; set; }
            public List<Parameter> Params { get; }
        }

        public class Parameter
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public string Attr { get; set; }
        }

        public class EnumData
        {
            public EnumData()
            {
                this.Vars = new();
            }
            public string Name { get; set; }
            public List<EnumVar> Vars { get; }
        }

        public class EnumVar
        {
            public string Name { get; set; }
            public string Val { get; set; }
        }

        public Dictionary<string, Guid> ClassIDs { get; }
        public Dictionary<string, Guid> InterfaceIDs { get; }
        public Dictionary<string, Guid> GUIDs { get; }
        public Dictionary<string, InterfaceData> Interfaces { get; }
        public Dictionary<string, StructData> Structs { get; }
        public Dictionary<string, EnumData> Enums { get; }
        public Dictionary<string, string> TypeDefs { get; }

        private InterfaceData currentInterface;
        private StructData currentStruct;
        private EnumData currentEnum;
        private MethodData currentMethod;
        private int lineNum;

        public Parser()
        {
            this.lineNum = 0; ;
            this.ClassIDs = new();
            this.InterfaceIDs = new();
            this.GUIDs = new();
            this.Interfaces = new();
            this.Structs = new();
            this.Enums = new();
            this.TypeDefs = new();
        }

        public void Parse(string file)
        {
            try
            {
                this.parserState = ParserState.NoState;

                using (StreamReader reader = new StreamReader(file))
                {
                    while (!reader.EndOfStream)
                    {
                        this.lineNum++;
                        string line = reader.ReadLine();
                        if (lineNum == 669)
                        {

                        }

                        ParseLine(line);                         
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debugger.Break();
            }
        }

        private void ParseLine(string line)
        {
            

            switch (this.parserState)
            {
            case ParserState.NoState:
                ParseNoState(line);
                break;
            case ParserState.Interface:
                ParseInterface(line);
                break;
            case ParserState.InterfaceParam:
                ParseInterfaceParam(line);
                break;
            case ParserState.Struct:
                ParseStruct(line);
                break;
            case ParserState.Enum:
                ParseEnum(line);
                break;
            }
        }

        private void ParseNoState(string line)
        {
            // Class IDs   
            var match = Regex.Match(line, @"DEFINE_GUID\(\s*CLSID_(?<name>[A-Z0-9_]+)\s*,\s*" + guidreg + @"\s*\);", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string name = match.Groups["name"].Value;
                Guid value = GetGuid(match);
                this.ClassIDs.Add(name, value);
                return;
            }

            // Interface IDs
            match = Regex.Match(line, @"DEFINE_GUID\(\s*IID_(?<name>[A-Z0-9_]+)\s*,\s*" + guidreg + @"\s*\);", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string name = match.Groups["name"].Value;
                Guid value = GetGuid(match);
                this.InterfaceIDs.Add(name, value);
                return;
            }

            // GUID
            match = Regex.Match(line, @"DEFINE_GUID\(\s*GUID_(?<name>[A-Z0-9_]+)\s*,\s*" + guidreg + @"\s*\);", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string name = match.Groups["name"].Value;
                Guid value = GetGuid(match);
                this.GUIDs.Add(name, value);
                return;
            }

            // IUnknown interface
            match = Regex.Match(line, @"DECLARE_INTERFACE_\((?<name>[A-Z0-9_]+)\s*,\s*(?<base>[A-Z0-9_]+)\)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string name = match.Groups["name"].Value;
                this.currentInterface = new() { Name = name };
                this.Interfaces.Add(name, this.currentInterface);
                this.parserState = ParserState.Interface;
                return;
            }

            // struct
            match = Regex.Match(line, @"\s*typedef\s*struct\s*(?<name>[A-Z0-9_]+)\s*$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                this.currentStruct = new(); 
                this.parserState = ParserState.Struct;
                return;
            }

            // enum
            match = Regex.Match(line, @"\s*typedef\s*enum", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                this.currentEnum = new();
                this.parserState = ParserState.Enum;
                return;
            }

            // typedef
            match = Regex.Match(line, @"\s*typedef\s*" + parareg + @"\s*;", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string type = match.Groups["type"].Value;
                string ptr = match.Groups["ptr"].Value;
                string name = match.Groups["name"].Value;
                this.TypeDefs[name] = string.IsNullOrEmpty(ptr) ? type : "ref " + type;
                return;
            }
        }

        private void ParseInterface(string line)
        {
            // reset on }
            if (Regex.IsMatch(line, @"\s*}"))
            {
                this.parserState = ParserState.NoState;
                return;
            }

            // empty method
            var match = Regex.Match(line, @"\s*STDMETHOD\((?<func>[A-Z0-9_]+)\)\s*\(THIS\)\s*PURE\s*;", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string func = match.Groups["func"].Value;
                this.currentInterface.Methods.Add(new MethodData() { Name = func });
                return;
            }

            // one param method
            match = Regex.Match(line, @"\s*STDMETHOD\((?<func>[A-Z0-9_]+)\)\s*\(THIS_\s*" + parareg + @"\s*\)\s*PURE\s*;", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string func = match.Groups["func"].Value;
                
                MethodData method = new() { Name = func };
                method.Params.Add(CreateParameter(match));
                this.currentInterface.Methods.Add(method);
                return;
            }

            // multi param method
            if (line.Contains("SetChannelPriority"))
            {
                match = Regex.Match(line, @"\s*STDMETHOD\(" + namereg + @"\)\s*\(THIS_\s*(" + parareg + @"\s*,?\s*)*\)\s*PURE\s*;", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string func = match.Groups["func"].Value;

                    MethodData method = new() { Name = func };
                    method.Params.Add(CreateParameter(match));
                    this.currentInterface.Methods.Add(method);
                    return;
                }
            }

            // multi line param method
            match = Regex.Match(line, @"\s*STDMETHOD\((?<func>[A-Z0-9_]+)\)\s*\(THIS_\s*" + parareg + @"\s*,", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string func = match.Groups["func"].Value;
                if (func != "QueryInterface")
                {
                    this.currentMethod = new() { Name = func };
                    this.currentMethod.Params.Add(CreateParameter(match));
                    this.currentInterface.Methods.Add(this.currentMethod);
                    this.parserState = ParserState.InterfaceParam;
                }
                return;
            }

            // method only line
            match = Regex.Match(line, @"\s*STDMETHOD\((?<func>[A-Z0-9_]+)\)\s*", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string func = match.Groups["func"].Value;
                if (func != "QueryInterface")
                {
                    this.currentMethod = new() { Name = func };
                    this.currentInterface.Methods.Add(this.currentMethod);
                    this.parserState = ParserState.InterfaceParam;
                }
                return;
            }
        }

        private void ParseInterfaceParam(string line)
        {
            // mid param
            var match = Regex.Match(line, @"\s*" + parareg + @"\s*,", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                this.currentMethod.Params.Add(CreateParameter(match));
                return;
            }

            // end param
            match = Regex.Match(line, @"\s*" + parareg + @"\s*\)\s*PURE\s*;", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                this.currentMethod.Params.Add(CreateParameter(match));

                this.parserState = ParserState.Interface;
                return;
            }

            // new line single param
            match = Regex.Match(line, @"\s*\(THIS_\s*" + parareg + @"\s*\)\s*PURE\s*;", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                this.currentMethod.Params.Add(CreateParameter(match));
                this.parserState = ParserState.Interface;
                return;
            }

            // new line first param
            match = Regex.Match(line, @"\s*\(THIS_\s*" + parareg + @"\s*,", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                this.currentMethod.Params.Add(CreateParameter(match));
                return;
            }
        }

        private void ParseStruct(string line)
        {
            // end of struct
            var match = Regex.Match(line, @"\s*}\s*(?<name>[A-Z0-9_]+)\s*;");
            if (match.Success)
            {
                string name = match.Groups["name"].Value;
                this.currentStruct.Name = name;
                this.Structs.Add(name, this.currentStruct);
                this.parserState = ParserState.NoState;
                return;
            }

            // parameter
            match = Regex.Match(line, @"\s*" + parareg + @"\s*;", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                this.currentStruct.Params.Add(CreateParameter(match));
                return;
            }
        }

        private void ParseEnum(string line)
        {
            // reset on }
            var match = Regex.Match(line, @"\s*}\s*(?<name>[A-Z0-9_]+)\s*;", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string name = match.Groups["name"].Value;
                this.currentEnum.Name = name;
                this.Enums.Add(name, this.currentEnum);
                this.parserState = ParserState.NoState;
                return;
            }

            // enum  and value
            match = Regex.Match(line, @"\s*(?<name>[A-Z0-9_]+)\s*=\s*(?<value>[A-Z0-9_]+)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string name = match.Groups["name"].Value;
                string value = match.Groups["value"].Value;
                this.currentEnum.Vars.Add(new EnumVar() { Name = name, Val = value });
                return;
            }
        }

        private Guid GetGuid(Match match)
        {
            uint a = uint.Parse(match.Groups["a"].Value, NumberStyles.HexNumber);
            ushort b = ushort.Parse(match.Groups["b"].Value, NumberStyles.HexNumber);
            ushort c = ushort.Parse(match.Groups["c"].Value, NumberStyles.HexNumber);
            byte d = byte.Parse(match.Groups["d"].Value, NumberStyles.HexNumber);
            byte e = byte.Parse(match.Groups["e"].Value, NumberStyles.HexNumber);
            byte f = byte.Parse(match.Groups["f"].Value, NumberStyles.HexNumber);
            byte g = byte.Parse(match.Groups["g"].Value, NumberStyles.HexNumber);
            byte h = byte.Parse(match.Groups["h"].Value, NumberStyles.HexNumber);
            byte i = byte.Parse(match.Groups["i"].Value, NumberStyles.HexNumber);
            byte j = byte.Parse(match.Groups["j"].Value, NumberStyles.HexNumber);
            byte k = byte.Parse(match.Groups["k"].Value, NumberStyles.HexNumber);
            return new Guid(a, b, c, d, e, f, g, h, i, j, k);
        }

        private Parameter CreateParameter(Match match)
        {
            string type = match.Groups["type"].Value;
            string ptr = match.Groups["ptr"].Value;
            string name = match.Groups["name"].Value;
            return type switch
            {
                "void*" => new Parameter() { Name = name, Type = "IntPtr" },
                "LPVOID" => new Parameter() { Name = name, Type = "IntPtr" },
                "LPUNKNOWN" => new Parameter() { Name = name, Type = "IntPtr" },
                "HANDLE" => new Parameter() { Name = name, Type = "IntPtr" },
                "HWND" => new Parameter() { Name = name, Type = "IntPtr" },
                "unsigned char" => new Parameter() { Name = name, Type = "Byte" },
                "BYTE" => new Parameter() { Name = name, Type = "Byte" },
                "short" => new Parameter() { Name = name, Type = "short" },
                "SHORT" => new Parameter() { Name = name, Type = "short" },
                "unsigned short" => new Parameter() { Name = name, Type = "ushort" },
                "WORD" => new Parameter() { Name = name, Type = "ushort" },
                "int" => new Parameter() { Name = name, Type = "int" },
                "INT" => new Parameter() { Name = name, Type = "int" },
                "unsigned int" => new Parameter() { Name = name, Type = "uint" },
                "UINT" => new Parameter() { Name = name, Type = "uint" },
                "long" => new Parameter() { Name = name, Type = "int" },
                "LONG" => new Parameter() { Name = name, Type = "int" },
                "unsigned long" => new Parameter() { Name = name, Type = "uint" },
                "ULONG" => new Parameter() { Name = name, Type = "uint" },
                "DWORD" => new Parameter() { Name = name, Type = "uint" },
                "LPDWORD" => new Parameter() { Name = name, Type = "ref uint" },
                "LPSTR" => new Parameter() { Name = name, Type = "string" },
                "LPCSTR" => new Parameter() { Name = name, Type = "string" },
                "LPWSTR" => new Parameter() { Name = name, Type = "string" },
                "LPCWSTR" => new Parameter() { Name = name, Type = "string" },
                "char*" => new Parameter() { Name = name, Type = "string" },
                "const char*" => new Parameter() { Name = name, Type = "string" },
                "float" => new Parameter() { Name = name, Type = "float" },
                "FLOAT" => new Parameter() { Name = name, Type = "float" },
                "double" => new Parameter() { Name = name, Type = "double" },
                "DOUBLE" => new Parameter() { Name = name, Type = "double" },
                "GUID" => new Parameter() { Name = name, Type = "Guid" },
                "REFGUID" => new Parameter() { Name = name, Type = "ref Guid" },
                "REFCLSID" => new Parameter() { Name = name, Type = "ref Guid" },
                "LPGUID" => new Parameter() { Name = name, Type = "ref Guid" },
                "BOOL" => new Parameter() { Name = name, Type = "bool", Attr = "[MarshalAs(UnmanagedType.Bool)]" },
                _ => CreateParameter(type, ptr, name)  
            };
        }

        private Parameter CreateParameter(string type, string ptr, string name)
        {
            Parameter parameter = new Parameter() { Name = name, Type = type };
            if (this.TypeDefs.TryGetValue(type, out string val))
            {
                if (val.StartsWith("ref") || string.IsNullOrEmpty(ptr))
                {
                    parameter.Type = val;
                }
                else
                {
                    parameter.Type = "ref " + val;
                }
            }
            else
            {
                if (type.StartsWith("LP"))
                {
                    parameter.Type = "ref " + type[2..];
                }
                else
                {
                    parameter.Type = type;
                }
            }
            return parameter;

        }

        private enum ParserState
        {
            NoState,
            Interface,
            InterfaceParam,
            Struct,
            Enum
        }
    }
}
