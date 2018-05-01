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
            if (e.NewElement != null && this.Control == null)
            {
                var control = new SKXamlCanvas();

                control.PaintSurface += (s, a) =>
                {
                    this.Element.OnPaintSurface?.Invoke(a.Surface, a.Info);
                };

                this.SetNativeControl(control);

                this.Element.SizeChanged += (s, a) => this.SetSize();
                this.SetSize();

                control.Invalidate();

                this.Element.Invalidated += this.OnInvalidated;
            }
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
