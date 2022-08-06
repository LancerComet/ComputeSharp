// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1_1.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.DirectX
{
    [Guid("1C4820BB-5771-4518-A581-2FE4DD0EC657")]
    [NativeTypeName("struct ID2D1ColorContext : ID2D1Resource")]
    [NativeInheritance("ID2D1Resource")]
    internal unsafe partial struct ID2D1ColorContext
    {
        public void** lpVtbl;
    }
}