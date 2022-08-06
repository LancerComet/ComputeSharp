// Copyright (c) Microsoft Corporation. All rights reserved.
//
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// Ported from https://github.dev/microsoft/Win2D/tree/master/winrt/lib/utils

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace ComputeSharp.D2D1.Uwp.Interop;

/// <summary>
/// An interop wrapper type for Win2D objects (see <see href="https://microsoft.github.io/Win2D/WinUI3/html/Interop.htm"/>).
/// </summary>
[Guid("5F10688D-EA55-4D55-A3B0-4DDB55C0C20A")]
[NativeTypeName("class ICanvasResourceWrapperNative : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe struct ICanvasResourceWrapperNative
{
    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasResourceWrapperNative*, Guid*, void**, int>)(lpVtbl[0]))((ICanvasResourceWrapperNative*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasResourceWrapperNative*, uint>)(lpVtbl[1]))((ICanvasResourceWrapperNative*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasResourceWrapperNative*, uint>)(lpVtbl[2]))((ICanvasResourceWrapperNative*)Unsafe.AsPointer(ref this));
    }

    /// <summary>
    /// Interface provided by various Canvas objects that is able to retrieve the wrapped resource.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(3)]
    [return: NativeTypeName("HRESULT")]
    public HRESULT GetNativeResource([NativeTypeName("ICanvasDevice*")] void* device, float dpi, [NativeTypeName("REFIID")] Guid* iid, void** resource)
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasResourceWrapperNative*, void*, float, Guid*, void**, int>)(lpVtbl[3]))((ICanvasResourceWrapperNative*)Unsafe.AsPointer(ref this), device, dpi, iid, resource);
    }
}
