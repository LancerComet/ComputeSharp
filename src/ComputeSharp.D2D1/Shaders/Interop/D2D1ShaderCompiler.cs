﻿using System;
using System.Buffers;
using System.Text;
using ComputeSharp.D2D1.Exceptions;
#if !NET6_0_OR_GREATER
using ComputeSharp.D2D1.NetStandard.System.Text;
#endif
using ComputeSharp.D2D1.Shaders.Translation;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// Provides methods to manually compile D2D1 shaders.
/// </summary>
public static class D2D1ShaderCompiler
{
    /// <summary>
    /// Compiles a new HLSL shader from the input source code.
    /// </summary>
    /// <param name="source">The HLSL source code to compile.</param>
    /// <param name="entryPoint">The entry point of the shader being compiled.</param>
    /// <param name="shaderProfile">The shader profile to use to compile the shader.</param>
    /// <param name="options">The compiler options to use to compile the shader.</param>
    /// <returns>The bytecode for the compiled shader.</returns>
    /// <exception cref="FxcCompilationException">Thrown if the compilation fails.</exception>
    public static ReadOnlyMemory<byte> Compile(
        ReadOnlySpan<char> source,
        ReadOnlySpan<char> entryPoint,
        D2D1ShaderProfile shaderProfile,
        D2D1ShaderCompilerOptions options)
    {
        // Encode the HLSL source to ASCII
        int maxSourceLength = Encoding.ASCII.GetMaxByteCount(source.Length);
        byte[] sourceBuffer = ArrayPool<byte>.Shared.Rent(maxSourceLength);
        int sourceWrittenBytes = Encoding.ASCII.GetBytes(source, sourceBuffer);

        // Encode the entry point to ASCII
        int maxEntryPointLength = Encoding.ASCII.GetMaxByteCount(entryPoint.Length);
        byte[] entryPointBuffer = ArrayPool<byte>.Shared.Rent(maxEntryPointLength);
        int entryPointWrittenBytes = Encoding.ASCII.GetBytes(entryPoint, entryPointBuffer);

        ReadOnlySpan<byte> sourceAscii = sourceBuffer.AsSpan(0, sourceWrittenBytes);
        ReadOnlySpan<byte> entryPointAscii = entryPointBuffer.AsSpan(0, entryPointWrittenBytes);

        return Compile(sourceAscii, entryPointAscii, shaderProfile, options);
    }

    /// <summary>
    /// Compiles a new HLSL shader from the input source code.
    /// </summary>
    /// <param name="source">The HLSL source code to compile (in ASCII).</param>
    /// <param name="entryPoint">The entry point of the shader being compiled (in ASCII).</param>
    /// <param name="shaderProfile">The shader profile to use to compile the shader.</param>
    /// <param name="options">The compiler options to use to compile the shader.</param>
    /// <returns>The bytecode for the compiled shader.</returns>
    /// <exception cref="FxcCompilationException">Thrown if the compilation fails.</exception>
    public static unsafe ReadOnlyMemory<byte> Compile(
        ReadOnlySpan<byte> source,
        ReadOnlySpan<byte> entryPoint,
        D2D1ShaderProfile shaderProfile,
        D2D1ShaderCompilerOptions options)
    {
        // Check linking support
        bool enableLinking = (options & D2D1ShaderCompilerOptions.EnableLinking) == D2D1ShaderCompilerOptions.EnableLinking;

        // Remove the linking flag to make the options blittable to flags
        options &= ~D2D1ShaderCompilerOptions.EnableLinking;

        // Compile the standalone D2D1 full shader
        using ComPtr<ID3DBlob> d3DBlobFullShader = D3DCompiler.CompileShader(
            source: source,
            macro: D3DCompiler.ASCII.D2D_FULL_SHADER,
            d2DEntry: entryPoint,
            entryPoint: entryPoint,
            target: D3DCompiler.ASCII.GetPixelShaderProfile(shaderProfile),
            flags: (uint)options);

        if (!enableLinking)
        {
            void* blobFullShaderPtr = d3DBlobFullShader.Get()->GetBufferPointer();
            nuint blobFullShaderSize = d3DBlobFullShader.Get()->GetBufferSize();

            return new Span<byte>(blobFullShaderPtr, (int)blobFullShaderSize).ToArray();
        }

        // Compile the export function
        using ComPtr<ID3DBlob> d3DBlobFunction = D3DCompiler.CompileShader(
            source: source,
            macro: D3DCompiler.ASCII.D2D_FUNCTION,
            d2DEntry: entryPoint,
            entryPoint: default,
            target: D3DCompiler.ASCII.GetLibraryProfile(shaderProfile),
            flags: (uint)options);

        // Embed it as private data if requested
        using ComPtr<ID3DBlob> d3DBlobLinked = D3DCompiler.SetD3DPrivateData(d3DBlobFullShader.Get(), d3DBlobFunction.Get());

        void* blobLinkedPtr = d3DBlobLinked.Get()->GetBufferPointer();
        nuint blobLinkedSize = d3DBlobLinked.Get()->GetBufferSize();

        return new Span<byte>(blobLinkedPtr, (int)blobLinkedSize).ToArray();
    }
}
