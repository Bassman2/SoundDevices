using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaDevices.IO.Internal.DirectSound.COMInterface
{
    [ComImport]
    [Guid("47D4D946-62E8-11CF-93BC-444553540000")]
    internal class DirectSound : IDirectSound
    {
    }

    [ComImport]
    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectSound
    {
        
            // IUnknown methods
            //STDMETHOD(QueryInterface)(THIS_ _In_ REFIID, _Outptr_ LPVOID *) PURE;
            //STDMETHOD_(ULONG, AddRef)(THIS) PURE;
            //STDMETHOD_(ULONG, Release)(THIS) PURE;

            //// IDirectSound methods
            //STDMETHOD(CreateSoundBuffer)(THIS_ _In_ LPCDSBUFFERDESC pcDSBufferDesc, _Outptr_ LPDIRECTSOUNDBUFFER * ppDSBuffer, _Pre_null_ LPUNKNOWN pUnkOuter) PURE;
            //STDMETHOD(GetCaps)(THIS_ _Out_ LPDSCAPS pDSCaps) PURE;
            //STDMETHOD(DuplicateSoundBuffer)(THIS_ _In_ LPDIRECTSOUNDBUFFER pDSBufferOriginal, _Outptr_ LPDIRECTSOUNDBUFFER * ppDSBufferDuplicate) PURE;
            //STDMETHOD(SetCooperativeLevel)(THIS_ HWND hwnd, DWORD dwLevel) PURE;
            //STDMETHOD(Compact)(THIS) PURE;
            //STDMETHOD(GetSpeakerConfig)(THIS_ _Out_ LPDWORD pdwSpeakerConfig) PURE;
            //STDMETHOD(SetSpeakerConfig)(THIS_ DWORD dwSpeakerConfig) PURE;
            //STDMETHOD(Initialize)(THIS_ _In_opt_ LPCGUID pcGuidDevice) PURE;
        

    }
}
