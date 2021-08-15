using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.DirectX.Internal
{
    [ComImport]
    [Guid("47D4D946-62E8-11CF-93BC-444553540000")]
    internal class DirectSound
    {
    }

    [ComImport]
    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectSound
    {
        //// IUnknown methods
        //void QueryInterface(IntPtr _this, Guid riid, ref IntPtr ppvObject);
        //void AddRef(IntPtr _this);
        //void Release(IntPtr _this);

        // IDirectSound methods
        //void CreateSoundBuffer([In] LPCDSBUFFERDESC pcDSBufferDesc, out LPDIRECTSOUNDBUFFER * ppDSBuffer, IntPtr pUnkOuter);
        //void GetCaps([Out] LPDSCAPS pDSCaps);
        //void DuplicateSoundBuffer([In] LPDIRECTSOUNDBUFFER pDSBufferOriginal, [Out] LPDIRECTSOUNDBUFFER * ppDSBufferDuplicate);
        //void SetCooperativeLevel(HWND hwnd, DWORD dwLevel);
        //void Compact();
        //void GetSpeakerConfig([Out] LPDWORD pdwSpeakerConfig);
        //void SetSpeakerConfig(DWORD dwSpeakerConfig);
        //void Initialize([In] LPCGUID pcGuidDevice);
        

    }
}
