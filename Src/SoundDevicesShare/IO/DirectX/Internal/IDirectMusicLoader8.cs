using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SoundDevices.IO.DirectX.Internal
{
    

    [ComImport]
    [Guid("19e7c08c-0a44-4e6a-a116-595a7cd5de8c")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDirectMusicLoader8
    {
        /* IUnknown */
        //void QueryInterface       (THIS_ REFIID, LPVOID FAR *) PURE;
        //STDMETHOD_(ULONG, AddRef)        (THIS) PURE;
        //  STDMETHOD_(ULONG, Release)       (THIS) PURE;

        /* IDirectMusicLoader */
        void GetObject(ref DMUS_OBJECTDESC pDesc, ref Guid riid, IntPtr ppv);
        void SetObject(ref DMUS_OBJECTDESC pDesc);
        void SetSearchDirectory(ref Guid rguidClass, string pwzPath, int fClear);
        void ScanDirectory(ref Guid rguidClass, string pwzFileExtension, string pwzScanFileName);
        void CacheObject(ref IDirectMusicObject pObject);
        void ReleaseObject(ref IDirectMusicObject pObject);
        void ClearCache(ref Guid rguidClass);
        void EnableCache(ref Guid rguidClass, int fEnable);
        void EnumObject(ref Guid rguidClass, int dwIndex, ref DMUS_OBJECTDESC pDesc);

        /* IDirectMusicLoader8 */
        void CollectGarbage();
        void ReleaseObjectByUnknown(IntPtr pObject);
        void LoadObjectFromFile(ref Guid rguidClassID, ref Guid iidInterfaceID, string pwzFilePath, ref IntPtr ppObject);
    }
}
