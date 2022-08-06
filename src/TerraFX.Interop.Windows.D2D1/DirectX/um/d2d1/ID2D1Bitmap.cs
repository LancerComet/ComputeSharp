// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.DirectX
{
    [Guid("A2296057-EA42-4099-983B-539FB6505426")]
    [NativeTypeName("struct ID2D1Bitmap : ID2D1Image")]
    [NativeInheritance("ID2D1Image")]
    internal unsafe partial struct ID2D1Bitmap
    {
        public void** lpVtbl;
    }
}