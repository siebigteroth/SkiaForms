using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using SkiaForms;
using SkiaSharp.Views.Android;
using Xamarin.Forms.Platform.Android;

namespace SkiaForms.Droid
{
    public class SkiaViewRenderer : ViewRenderer<SkiaView, SKCanvasView>
    {
        protected SkiaViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SkiaView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (this.Control == null)
                {
                    var control = new SKCanvasView(this.Context);
                    control.PaintSurface += this.OnPaintSurface;
                    this.SetNativeControl(control);
                    control.Invalidate();
                }

                e.NewElement.SizeChanged += (s, a) => this.SetSize();
                this.SetSize();

                e.NewElement.Invalidated += this.OnInvalidated;
            }
            else if (e.OldElement != null)
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

        private void OnInvalidated(object sender, System.EventArgs e)
        {
            this.Control?.Invalidate();
        }

        private void SetSize()
        {
            if (this.Control != null)
            {
                this.Control.SetMinimumHeight((int)this.Element.Height);
                this.Control.SetMinimumWidth((int)this.Element.Width);
            }
        }
    }
}
