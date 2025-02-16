using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#if NET6_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif

namespace ComputeSharp;

/// <summary>
/// Packed pixel type containing four 16-bit unsigned normalized values ranging from 0 to 255.
/// The color components are stored in red, green, blue, and alpha order (least significant to most significant byte).
/// <para>
/// Ranges from [0, 0, 0, 0] to [1, 1, 1, 1] in vector form.
/// </para>
/// </summary>
/// <remarks>This struct is fully mutable.</remarks>
[StructLayout(LayoutKind.Sequential)]
public struct Rgba64 : IEquatable<Rgba64>, IPixel<Rgba64, Float4>
#if NET6_0_OR_GREATER
    , ISpanFormattable
#endif
{
    /// <summary>
    /// The red component.
    /// </summary>
    public ushort R;

    /// <summary>
    /// The green component.
    /// </summary>
    public ushort G;

    /// <summary>
    /// The blue component.
    /// </summary>
    public ushort B;

    /// <summary>
    /// The alpha component.
    /// </summary>
    public ushort A;

    /// <summary>
    /// Initializes a new instance of the <see cref="Rgba64"/> struct.
    /// </summary>
    /// <param name="r">The red component.</param>
    /// <param name="g">The green component.</param>
    /// <param name="b">The blue component.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rgba64(ushort r, ushort g, ushort b)
    {
        this.R = r;
        this.G = g;
        this.B = b;
        this.A = ushort.MaxValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rgba64"/> struct.
    /// </summary>
    /// <param name="r">The red component.</param>
    /// <param name="g">The green component.</param>
    /// <param name="b">The blue component.</param>
    /// <param name="a">The alpha component.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rgba64(ushort r, ushort g, ushort b, ushort a)
    {
        this.R = r;
        this.G = g;
        this.B = b;
        this.A = a;
    }

    /// <summary>
    /// Gets or sets the packed representation of the <see cref="Rgba64"/> struct.
    /// </summary>
    public ulong PackedValue
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get => Unsafe.As<Rgba64, ulong>(ref Unsafe.AsRef(in this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => Unsafe.As<Rgba64, ulong>(ref this) = value;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Float4 ToPixel()
    {
#if NET6_0_OR_GREATER
        if (Sse2.IsSupported)
        {
            long pack = Unsafe.As<Rgba64, long>(ref Unsafe.AsRef(in this));
            Vector128<ushort> vUShort = Vector128.CreateScalarUnsafe(pack).AsUInt16();
            Vector128<int> vInt = Sse2.UnpackLow(vUShort, Vector128<ushort>.Zero).AsInt32();
            Vector128<float> vFloat = Sse2.ConvertToVector128Single(vInt);
            Vector128<float> vMax = Vector128.Create((float)ushort.MaxValue);
            Vector128<float> vNorm = Sse.Divide(vFloat, vMax);

            return vNorm.AsVector4();
        }
#endif
        Vector4 linear = new(this.R, this.G, this.B, this.A);
        Vector4 normalized = linear / ushort.MaxValue;

        return normalized;
    }

    /// <summary>
    /// Compares two <see cref="Rgba64"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Rgba64"/> on the left side of the operand.</param>
    /// <param name="right">The <see cref="Rgba64"/> on the right side of the operand.</param>
    /// <returns>
    /// True if the <paramref name="left"/> parameter is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Rgba64 left, Rgba64 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="Rgba64"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Rgba64"/> on the left side of the operand.</param>
    /// <param name="right">The <see cref="Rgba64"/> on the right side of the operand.</param>
    /// <returns>
    /// True if the <paramref name="left"/> parameter is not equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Rgba64 left, Rgba64 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override readonly bool Equals(object? obj)
    {
        return obj is Rgba64 rgba32 && Equals(rgba32);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool Equals(Rgba64 other)
    {
        return PackedValue.Equals(other.PackedValue);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override readonly int GetHashCode()
    {
        return PackedValue.GetHashCode();
    }

    /// <inheritdoc/>
    public override readonly string ToString()
    {
        return $"{nameof(Rgba64)}({this.R}, {this.G}, {this.B}, {this.A})";
    }

#if NET6_0_OR_GREATER
    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return string.Create(formatProvider, $"{nameof(Rgba64)}({this.R}, {this.G}, {this.B}, {this.A})");
    }

    /// <inheritdoc/>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        return destination.TryWrite(provider, $"{nameof(Rgba64)}({this.R}, {this.G}, {this.B}, {this.A})", out charsWritten);
    }
#endif
}