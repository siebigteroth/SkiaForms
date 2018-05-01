using SkiaForms;
using SkiaSharp.Views.Mac;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

[assembly: ExportRenderer(typeof(SkiaView), typeof(SkiaForms.macOS.SkiaViewRenderer))]

namespace SkiaForms.macOS
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
                    control.PaintSurface += this.OnPaintSurface;
                    this.SetNativeControl(control);
                }

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
            if (this.Control != null)
            {
                this.Control.NeedsDisplay = true;
            }
        }
    }
}
