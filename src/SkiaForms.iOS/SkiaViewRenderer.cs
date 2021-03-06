﻿using System;
using SkiaForms;
using SkiaSharp.Views.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SkiaView), typeof(SkiaForms.iOS.SkiaViewRenderer))]

namespace SkiaForms.iOS
{
    public class SkiaViewRenderer : ViewRenderer<SkiaView, SKCanvasView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SkiaView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (this.Control == null)
                {
                    var control = new SKCanvasView();
                    control.Opaque = false;
                    control.PaintSurface += this.OnPaintSurface;
                    this.SetNativeControl(control);
                    control.SetNeedsDisplay();
                }

                e.NewElement.Invalidated += this.OnInvalidated;
            }
            else if(e.OldElement != null)
            {
                e.OldElement.Invalidated -= this.OnInvalidated;
                if (this.Control != null)
                {
                    this.Control.PaintSurface -= this.OnPaintSurface;
                }
            }
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            this.Element.OnPaintSurface?.Invoke(e.Surface, e.Info);
        }

        private void OnInvalidated(object sender, EventArgs e)
        {
            this.Control?.SetNeedsDisplay();
        }
    }
}
