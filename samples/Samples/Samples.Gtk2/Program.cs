using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

namespace Samples.Gtk2
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            SkiaForms.Gtk2.Init.Include();
            Gtk.Application.Init();
            Forms.Init();
            var app = new App();
            var window = new FormsWindow();
            window.LoadApplication(app);
            window.SetApplicationTitle("GTK2 Sample");
            window.Show();
            Gtk.Application.Run();
        }
    }
}
