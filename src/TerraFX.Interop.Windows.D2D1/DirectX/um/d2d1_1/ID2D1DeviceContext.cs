// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1_1.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D2D1_COMPOSITE_MODE;
using static TerraFX.Interop.DirectX.D2D1_INTERPOLATION_MODE;

#pragma warning disable CS0649

namespace TerraFX.Interop.DirectX
{
    [Guid("E8F7FE7A-191C-466D-AD95-975678BDA998")]
    [NativeTypeName("struct ID2D1DeviceContext : ID2D1RenderTarget")]
    [NativeInheritance("ID2D1RenderTarget")]
    internal unsafe partial struct ID2D1DeviceContext
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public void GetFactory(ID2D1Factory** factory)
        {
            ((delegate* unmanaged[Stdcall]<ID2D1DeviceContext*, ID2D1Factory**, void>)(lpVtbl[3]))((ID2D1DeviceContext*)Unsafe.AsPointer(ref this), factory);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(57)]
        public HRESULT CreateBitmap([NativeTypeName("D2D1_SIZE_U")] D2D_SIZE_U size, [NativeTypeName("const void *")] void* sourceData, [NativeTypeName("UINT32")] uint pitch, [NativeTypeName("const D2D1_BITMAP_PROPERTIES1 *")] D2D1_BITMAP_PROPERTIES1* bitmapProperties, ID2D1Bitmap1** bitmap)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1DeviceContext*, D2D_SIZE_U, void*, uint, D2D1_BITMAP_PROPERTIES1*, ID2D1Bitmap1**, int>)(lpVtbl[57]))((ID2D1DeviceContext*)Unsafe.AsPointer(ref this), size, sourceData, pitch, bitmapProperties, bitmap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(63)]
        public HRESULT CreateEffect([NativeTypeName("const IID &")] Guid* effectId, ID2D1Effect** effect)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1DeviceContext*, Guid*, ID2D1Effect**, int>)(lpVtbl[63]))((ID2D1DeviceContext*)Unsafe.AsPointer(ref this), effectId, effect);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(70)]
        public HRESULT GetImageLocalBounds(ID2D1Image* image, [NativeTypeName("D2D1_RECT_F *")] D2D_RECT_F* localBounds)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1DeviceContext*, ID2D1Image*, D2D_RECT_F*, int>)(lpVtbl[70]))((ID2D1DeviceContext*)Unsafe.AsPointer(ref this), image, localBounds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(74)]
        public void SetTarget(ID2D1Image* image)
        {
            ((delegate* unmanaged[Stdcall]<ID2D1DeviceContext*, ID2D1Image*, void>)(lpVtbl[74]))((ID2D1DeviceContext*)Unsafe.AsPointer(ref this), image);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(75)]
        public void GetTarget(ID2D1Image** image)
        {
            ((delegate* unmanaged[Stdcall]<ID2D1DeviceContext*, ID2D1Image**, void>)(lpVtbl[75]))((ID2D1DeviceContext*)Unsafe.AsPointer(ref this), image);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(83)]
        public void DrawImage(ID2D1Image* image, [NativeTypeName("const D2D1_POINT_2F *")] D2D_POINT_2F* targetOffset = null, [NativeTypeName("const D2D1_RECT_F *")] D2D_RECT_F* imageRectangle = null, D2D1_INTERPOLATION_MODE interpolationMode = D2D1_INTERPOLATION_MODE_LINEAR, D2D1_COMPOSITE_MODE compositeMode = D2D1_COMPOSITE_MODE_SOURCE_OVER)
        {
            ((delegate* unmanaged[Stdcall]<ID2D1DeviceContext*, ID2D1Image*, D2D_POINT_2F*, D2D_RECT_F*, D2D1_INTERPOLATION_MODE, D2D1_COMPOSITE_MODE, void>)(lpVtbl[83]))((ID2D1DeviceContext*)Unsafe.AsPointer(ref this), image, targetOffset, imageRectangle, interpolationMode, compositeMode);
        }
    }
}