// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1_1.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("A898A84C-3873-4588-B08B-EBBF978DF041")]
    [NativeTypeName("struct ID2D1Bitmap1 : ID2D1Bitmap")]
    [NativeInheritance("ID2D1Bitmap")]
    internal unsafe partial struct ID2D1Bitmap1
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Bitmap1*, Guid*, void**, int>)(lpVtbl[0]))((ID2D1Bitmap1*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Bitmap1*, uint>)(lpVtbl[1]))((ID2D1Bitmap1*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Bitmap1*, uint>)(lpVtbl[2]))((ID2D1Bitmap1*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public HRESULT CopyFromBitmap([NativeTypeName("const D2D1_POINT_2U *")] D2D_POINT_2U* destPoint, ID2D1Bitmap* bitmap, [NativeTypeName("const D2D1_RECT_U *")] D2D_RECT_U* srcRect)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Bitmap1*, D2D_POINT_2U*, ID2D1Bitmap*, D2D_RECT_U*, int>)(lpVtbl[8]))((ID2D1Bitmap1*)Unsafe.AsPointer(ref this), destPoint, bitmap, srcRect);
        }
    }
}