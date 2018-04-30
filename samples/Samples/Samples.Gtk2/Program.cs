using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using Xamarin.Forms.Platform.GTK.Helpers;

namespace Samples.Gtk2
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            GtkThemes.Init();
            if (PlatformHelper.GetGTKPlatform() == GTKPlatform.Windows)
            {
                GtkThemes.LoadCustomTheme("Themes/gtkrc-dark");
            }

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
