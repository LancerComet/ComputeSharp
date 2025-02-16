// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1_1.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace TerraFX.Interop.DirectX;

[Guid("28211A43-7D89-476F-8181-2D6159B220AD")]
[NativeTypeName("struct ID2D1Effect : ID2D1Properties")]
[NativeInheritance("ID2D1Properties")]
internal unsafe partial struct ID2D1Effect
{
    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(9)]
    public HRESULT SetValue([NativeTypeName("UINT32")] uint index, D2D1_PROPERTY_TYPE type, [NativeTypeName("const BYTE *")] byte* data, [NativeTypeName("UINT32")] uint dataSize)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1Effect*, uint, D2D1_PROPERTY_TYPE, byte*, uint, int>)(lpVtbl[9]))((ID2D1Effect*)Unsafe.AsPointer(ref this), index, type, data, dataSize);
    }
}