using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Uwp.Interop;
using Microsoft.Graphics.Canvas;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using Win32 = TerraFX.Interop.Windows.Windows;

namespace ComputeSharp.D2D1.Uwp;

/// <summary>
/// Extensions for the <see cref="CanvasDrawingSession"/> type.
/// </summary>
public static class CanvasDrawingSessionExtensions
{
    /// <summary>
    /// Draws a custom D2D1 pixel shader onto a target <see cref="CanvasDrawingSession"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to draw.</typeparam>
    public static unsafe void DrawEffect<T>(this CanvasDrawingSession session)
        where T : unmanaged, ID2D1PixelShader
    {
        // TODO: validate arguments

        using ComPtr<IUnknown> canvasDrawingSessionUnknown = default;

        // Get the underlying IUnknown* object for the canvas drawing session
        canvasDrawingSessionUnknown.Attach((IUnknown*)Marshal.GetIUnknownForObject(session));

        using ComPtr<ICanvasResourceWrapperNative> canvasResourceWrapperNative = default;

        // Query for ICanvasResourceWrapperNative. This is the interop wrapping type the canvas
        // drawing session supports, which can be used to get the underlying drawing session.
        canvasDrawingSessionUnknown.CopyTo(canvasResourceWrapperNative.GetAddressOf()).Assert();

        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = default;

        // Get the underlying ID2D1DeviceContext* object from the wrapper
        canvasResourceWrapperNative.Get()->GetNativeResource(
            device: null,
            dpi: 0,
            iid: Win32.__uuidof<ID2D1DeviceContext>(),
            resource: d2D1DeviceContext.GetVoidAddressOf()).Assert();

        using ComPtr<ID2D1Factory> d2D1Factory = default;

        // Get the associated ID2D1Factory* for the drawing context
        d2D1DeviceContext.Get()->GetFactory(d2D1Factory.GetAddressOf());

        using ComPtr<ID2D1Factory1> d2DFactory1 = default;

        // Query for ID2D1Factory1, which is the type with the additional APIs that are needed
        d2D1Factory.CopyTo(d2DFactory1.GetAddressOf()).Assert();

        // Register the effect for the target factory
        D2D1PixelShaderEffect.RegisterForD2D1Factory1<T>(d2DFactory1.Get(), out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        // Create the ID2D1Effect object for the input D2D1 pixel shader
        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<T>(d2D1DeviceContext.Get(), d2D1Effect.GetVoidAddressOf());

        using ComPtr<ID2D1Image> d2D1ImageOriginalTarget = default;

        // Get the current render target, which will contain the current drawing session output. This
        // will become the input of the input D2D1 pixel shader, so the effect can work on those pixels.
        d2D1DeviceContext.Get()->GetTarget(d2D1ImageOriginalTarget.GetAddressOf());

        D2D_RECT_F d2DRectOriginalTargetBounds = default;

        // Calculate the bounds of the original render target, so we can create a bitmap of the same size
        d2D1DeviceContext.Get()->GetImageLocalBounds(d2D1ImageOriginalTarget.Get(), &d2DRectOriginalTargetBounds).Assert();

        D2D_SIZE_U d2DSizeEffectTarget;
        d2DSizeEffectTarget.width = (uint)d2DRectOriginalTargetBounds.right;
        d2DSizeEffectTarget.height = (uint)d2DRectOriginalTargetBounds.bottom;

        D2D1_BITMAP_PROPERTIES1 d2DBitmapProperties1EffectTarget = default;
        d2DBitmapProperties1EffectTarget.pixelFormat.format = DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM;
        d2DBitmapProperties1EffectTarget.pixelFormat.alphaMode = D2D1_ALPHA_MODE.D2D1_ALPHA_MODE_PREMULTIPLIED;
        d2DBitmapProperties1EffectTarget.bitmapOptions =
            D2D1_BITMAP_OPTIONS.D2D1_BITMAP_OPTIONS_TARGET |
            D2D1_BITMAP_OPTIONS.D2D1_BITMAP_OPTIONS_CANNOT_DRAW;

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1EffectTarget = default;

        // Create a bitmap image to be the render target of the effect. This will receive
        // the results of applying the effect using the previous render target as input.
        d2D1DeviceContext.Get()->CreateBitmap(
            size: d2DSizeEffectTarget,
            sourceData: null,
            pitch: 0,
            bitmapProperties: &d2DBitmapProperties1EffectTarget,
            bitmap: d2D1Bitmap1EffectTarget.GetAddressOf()).Assert();

        // Set the new bitmap as render target
        d2D1DeviceContext.Get()->SetTarget((ID2D1Image*)d2D1Bitmap1EffectTarget.Get());

        using ComPtr<ID2D1Image> d2D1ImageEffectOutput = default;

        // Get the output image for the effect to apply
        d2D1Effect.Get()->GetOutput(d2D1ImageEffectOutput.GetAddressOf());

        // Set the original render target as input for the effect
        d2D1Effect.Get()->SetInput(index: 0, input: d2D1ImageOriginalTarget.Get(), invalidate: 1);

        // Apply the effect, which draws on the new bitmap we just created
        d2D1DeviceContext.Get()->DrawImage(
            image: d2D1ImageEffectOutput.Get(),
            targetOffset: null,
            imageRectangle: null,
            interpolationMode: D2D1_INTERPOLATION_MODE.D2D1_INTERPOLATION_MODE_NEAREST_NEIGHBOR,
            compositeMode: D2D1_COMPOSITE_MODE.D2D1_COMPOSITE_MODE_SOURCE_COPY);

        // Set the original target again, so the drawing session can keep drawing to it
        d2D1DeviceContext.Get()->SetTarget(d2D1ImageOriginalTarget.Get());

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1OriginalTarget = default;

        // Query an ID2D1Bitmap1 object from the original target, so we can call ID2D1Bitmap1::CopyFromBitmap
        d2D1ImageOriginalTarget.CopyTo(d2D1Bitmap1OriginalTarget.GetAddressOf()).Assert();

        D2D_POINT_2U d2DPointCopyDestination = default;
        D2D_RECT_U d2DRectCopySource = default;
        d2DRectCopySource.left = (uint)d2DRectOriginalTargetBounds.left;
        d2DRectCopySource.top = (uint)d2DRectOriginalTargetBounds.top;
        d2DRectCopySource.right = (uint)d2DRectOriginalTargetBounds.right;
        d2DRectCopySource.bottom = (uint)d2DRectOriginalTargetBounds.bottom;

        // Copy the results drawn on the bitmap back to the original render target
        d2D1Bitmap1OriginalTarget.Get()->CopyFromBitmap(
            destPoint: &d2DPointCopyDestination,
            bitmap: (ID2D1Bitmap*)d2D1Bitmap1EffectTarget.Get(),
            srcRect: &d2DRectCopySource).Assert();
    }
}
