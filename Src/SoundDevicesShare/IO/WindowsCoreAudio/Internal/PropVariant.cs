using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.WindowsCoreAudio.Internal
{
    [StructLayout(LayoutKind.Explicit)]
    public struct PropVariant
    {
        [FieldOffset(0)] short vt;
        [FieldOffset(2)] short wReserved1;
        [FieldOffset(4)] short wReserved2;
        [FieldOffset(6)] short wReserved3;
        [FieldOffset(8)] sbyte cVal;
        [FieldOffset(8)] byte bVal;
        [FieldOffset(8)] short iVal;
        [FieldOffset(8)] ushort uiVal;
        [FieldOffset(8)] int lVal;
        [FieldOffset(8)] uint ulVal;
        [FieldOffset(8)] long hVal;
        [FieldOffset(8)] ulong uhVal;
        [FieldOffset(8)] float fltVal;
        [FieldOffset(8)] double dblVal;
        [FieldOffset(8)] Blob blobVal;
        [FieldOffset(8)] DateTime date;
        [FieldOffset(8)] bool boolVal;
        [FieldOffset(8)] int scode;
        [FieldOffset(8)] System.Runtime.InteropServices.ComTypes.FILETIME filetime;
        [FieldOffset(8)] IntPtr ptr;


        public bool IsEmpty => (VarEnum)vt == VarEnum.VT_EMPTY;

        public static implicit operator int(PropVariant propVariant) => propVariant.lVal;
        public static implicit operator string(PropVariant propVariant) => propVariant.ToString();

        //I'm sure there is a more efficient way to do this but this works ..for now..
        internal byte[] GetBlob()
        {
            byte[] Result = new byte[blobVal.Length];
            for (int i = 0; i < blobVal.Length; i++)
            {
                Result[i] = Marshal.ReadByte((IntPtr)((long)(blobVal.Data) + i));
            }
            return Result;
        }

        public object Value
        {
            get
            {
                return (VarEnum)vt switch
                {
                    VarEnum.VT_EMPTY => string.Empty,
                    VarEnum.VT_NULL => null,
                    VarEnum.VT_I1 => bVal,
                    VarEnum.VT_I2 => iVal,
                    VarEnum.VT_I4 => lVal,
                    VarEnum.VT_I8 => hVal,
                    VarEnum.VT_INT => iVal,
                    VarEnum.VT_UI4 => ulVal,
                    VarEnum.VT_LPWSTR => Marshal.PtrToStringUni(ptr),
                    VarEnum.VT_BLOB => GetBlob(),
                    _ => $"FIXME Type = {(VarEnum)vt}"
                };
            }
        }

        public override string ToString()
        {
            return (VarEnum)vt switch
            {
                VarEnum.VT_EMPTY => string.Empty,
                VarEnum.VT_NULL => null,
                VarEnum.VT_I1 => bVal.ToString(),
                VarEnum.VT_I2 => iVal.ToString(),
                VarEnum.VT_I4 => lVal.ToString(),
                VarEnum.VT_I8 => hVal.ToString(),
                VarEnum.VT_INT => iVal.ToString(),
                VarEnum.VT_UI4 => ulVal.ToString(),
                VarEnum.VT_LPWSTR => Marshal.PtrToStringUni(ptr),
                //VarEnum.VT_BLOB => GetBlob(),
                _ => $"FIXME Type = {(VarEnum)vt}"
            };
        }
    }
}
