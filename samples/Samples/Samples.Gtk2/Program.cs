using System;
using SkiaForms.Gtk2;
using SkiaSharp;
using SkiaSharp.Views.Gtk;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

namespace Samples.Gtk2
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var x = new Type[]
            {
                typeof(SkiaViewRenderer),
                typeof(SKWidget),
                typeof(SKSurface),
                typeof(SkiaSharp.Views.Desktop.SKControl)
            };

            Gtk.Application.Init();
            Forms.Init();
            var app = new App();
            var window = new FormsWindow();
            window.LoadApplication(app);
            window.SetApplicationTitle("GTK2 Samples");
            window.Show();
            Gtk.Application.Run();
        }
    }
}
