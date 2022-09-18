﻿using System;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing gathered resource texture descriptions for a shader.
/// </summary>
/// <param name="ResourceTextureDescriptions">The resource textures for the current shader.</param>
internal sealed record ResourceTextureDescriptionsInfo(ImmutableArray<ResourceTextureDescription> ResourceTextureDescriptions)
{
    /// <inheritdoc/>
    public bool Equals(ResourceTextureDescriptionsInfo? obj) => Comparer.Default.Equals(this, obj);

    /// <inheritdoc/>
    public override int GetHashCode() => Comparer.Default.GetHashCode(this);

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="ResourceTextureDescriptionsInfo"/>.
    /// </summary>
    private sealed class Comparer : Comparer<ResourceTextureDescriptionsInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, ResourceTextureDescriptionsInfo obj)
        {
            hashCode.AddRange(obj.ResourceTextureDescriptions);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(ResourceTextureDescriptionsInfo x, ResourceTextureDescriptionsInfo y)
        {
            return x.ResourceTextureDescriptions.SequenceEqual(y.ResourceTextureDescriptions);
        }
    }
}