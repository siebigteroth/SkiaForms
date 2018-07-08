using System;
using SkiaSharp;
using Xamarin.Forms;

namespace SkiaForms
{
    public class SkiaView : View
    {
        public Action<SKSurface, SKImageInfo> OnPaintSurface { get; set; }

        public event EventHandler Invalidated;

        public void Invalidate()
        {
            this.Invalidated?.Invoke(this, new EventArgs());
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return new SizeRequest(new Size(widthConstraint, heightConstraint));
        }
    }
}
