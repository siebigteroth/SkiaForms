using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaForms;
using SkiaSharp;
using Xamarin.Forms;

namespace Samples
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            this.DrawCircle();
		}

        private void DrawCircle()
        {
            var skiaView = new SkiaView()
            {
                WidthRequest = 40,
                HeightRequest = 40
            };

            skiaView.OnPaintSurface = (surface, imageInfo) =>
            {
                var centerX = 20;
                var centerY = 20;

                var canvas = surface.Canvas;
                canvas.Clear();

                var fill = new SKPaint()
                {
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    Color = SKColor.Parse("#ff0000")
                };

                canvas.DrawCircle(centerX, centerY, 20, fill);
            };

            this.layout.Children.Add(skiaView);
        }
	}
}
