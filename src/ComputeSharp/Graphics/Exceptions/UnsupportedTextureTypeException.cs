using System;
using System.Text;
using CommunityToolkit.Diagnostics;

namespace ComputeSharp.Exceptions;

/// <summary>
/// A custom <see cref="NotSupportedException"/> that indicates when a texture was attempted to be created with an unsupported type.
/// </summary>
public sealed class UnsupportedTextureTypeException : NotSupportedException
{
    /// <summary>
    /// Creates a new <see cref="UnsupportedTextureTypeException"/> instance.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    private UnsupportedTextureTypeException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Creates a new <see cref="UnsupportedTextureTypeException"/> instance from the specified parameters.
    /// </summary>
    /// <param name="rank">The rank of the texture that couldn't be created.</param>
    /// <param name="type">The element type of texture that was being created.</param>
    /// <returns>A new <see cref="UnsupportedTextureTypeException"/> instance with a formatted error message.</returns>
    private static UnsupportedTextureTypeException Create(int rank, Type type)
    {
        StringBuilder builder = new(256);

        _ = builder.AppendLine($"The device in use does not support creating {rank}D textures of type {type}.");
        _ = builder.Append($"Make sure to check the support at runtime by using {nameof(GraphicsDevice)}.");

        _ = builder.AppendLine(rank switch
        {
            1 => $"{nameof(GraphicsDevice.IsReadOnlyTexture1DSupportedForType)}<T>() or {nameof(GraphicsDevice)}.{nameof(GraphicsDevice.IsReadWriteTexture1DSupportedForType)}<T>().",
            2 => $"{nameof(GraphicsDevice.IsReadOnlyTexture2DSupportedForType)}<T>() or {nameof(GraphicsDevice)}.{nameof(GraphicsDevice.IsReadWriteTexture2DSupportedForType)}<T>().",
            3 => $"{nameof(GraphicsDevice.IsReadOnlyTexture3DSupportedForType)}<T>() or {nameof(GraphicsDevice)}.{nameof(GraphicsDevice.IsReadWriteTexture3DSupportedForType)}<T>().",
            _ => ThrowHelper.ThrowArgumentException<string>("Invalid texture rank.")
        });

        _ = builder.Append("As a possible workaround on older devices, consider using a texture type of lower rank, or a linear buffer.");

        return new(builder.ToString());
    }

    /// <summary>
    /// Throws a new <see cref="UnsupportedTextureTypeException"/> instance from the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of values in the texture that couldn't be created.</typeparam>
    internal static void ThrowForTexture1D<T>()
        where T : unmanaged
    {
        throw Create(1, typeof(T));
    }

    /// <summary>
    /// Throws a new <see cref="UnsupportedTextureTypeException"/> instance from the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of values in the texture that couldn't be created.</typeparam>
    internal static void ThrowForTexture2D<T>()
        where T : unmanaged
    {
        throw Create(2, typeof(T));
    }

    /// <summary>
    /// Throws a new <see cref="UnsupportedTextureTypeException"/> instance from the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of values in the texture that couldn't be created.</typeparam>
    internal static void ThrowForTexture3D<T>()
        where T : unmanaged
    {
        throw Create(3, typeof(T));
    }
}