using SkiaForms;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.Gtk;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

[assembly: ExportRenderer(typeof(SkiaView), typeof(SkiaForms.Gtk2.SkiaViewRenderer))]

namespace SkiaForms.Gtk2
{
    public class SkiaViewRenderer : ViewRenderer<SkiaView, SKWidget>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SkiaView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (this.Control == null)
                {
                    var control = new SKWidget();
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
            this.Control?.QueueDraw();
        }

        private void SetSize()
        {
            if (this.Control != null)
            {
                this.Control.HeightRequest = (int)this.Element.Height;
                this.Control.WidthRequest = (int)this.Element.Width;
            }
        }
    }
}
