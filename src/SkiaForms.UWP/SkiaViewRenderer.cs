using SkiaForms;
using SkiaSharp.Views.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(SkiaView), typeof(SkiaForms.UWP.SkiaViewRenderer))]

namespace SkiaForms.UWP
{
    public class SkiaViewRenderer : ViewRenderer<SkiaView, SKXamlCanvas>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SkiaView> e)
        {
            if (e.NewElement != null)
            {
                if (this.Control == null)
                {
                    var control = new SKXamlCanvas();
                    control.PaintSurface += this.OnPaintSurface;
                    this.SetNativeControl(control);
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
            if (this.Control != null && this.Element.Height >= 0)
            {
                this.Control.Height = this.Element.Height;
            }

            if (this.Control != null && this.Element.Width >= 0)
            {
                this.Control.Width = this.Element.Width;
            }
        }
    }
}
