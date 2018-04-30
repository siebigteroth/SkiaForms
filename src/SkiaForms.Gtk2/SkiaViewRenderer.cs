using Gdk;
using SkiaForms;
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

            if (e.NewElement != null && this.Control == null)
            {
                var widget = new SKWidget()
                {
                    Name = "skiaView",
                    Visible = true,
                    WidthRequest = 40,
                    HeightRequest = 40
                };

                widget.PaintSurface += (s, a) => this.Element.OnPaintSurface?.Invoke(a.Surface, a.Info);

                this.SetNativeControl(widget);

                this.Element.SizeChanged += (s, a) => this.SetSize();
                this.SetSize();

                this.Element.Invalidated += this.OnInvalidated;
            }
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
