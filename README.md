# SkiaForms
Lightweight SkiaSharp renderers for Xamarin.Forms

## Renderers
The following renderers are currently available:
* UWP
* GTK (Windows and Linux tested; macOS not tested)
* iOS
* macOS
* Android

## Install
To install this library, you can use the following nuget package:
[coming soon](https://www.nuget.org/)

### Linux
To run SkiaSharp (v1.60.0) on a Mono application on Debian, the following dependencies have to be installed besides mono:
```
sudo apt-get install gtk-sharp2

wget https://github.com/mono/SkiaSharp/releases/download/v1.60.0/libSkiaSharp.so
sudo mkdir /usr/lib/cli/skiasharp
sudo cp ./libSkiaSharp.so /usr/lib/cli/skiasharp/

sudo nano /etc/mono/config
```

Add the following lines inside the configuration-tag and save the file:
```
<dllmap dll="libglib-2.0-0.dll" target="libglib-2.0.so.0" os="linux"/>
<dllmap dll="libgtk-win32-2.0-0.dll" target="libgtk-x11-2.0.so.0" os="!windows"/>
<dllmap dll="libgdk-win32-2.0-0.dll" target="libgdk-x11-2.0.so.0" os="!windows"/>
<dllmap dll="libgdk_pixbuf-2.0-0.dll" target="libgdk_pixbuf-2.0.so.0" os="!windows"/>
<dllmap dll="gdksharpglue-2" target="/usr/lib/cli/gdk-sharp-2.0/libgdksharpglue-2.so" os="!windows"/>
<dllmap dll="gtksharpglue-2" target="/usr/lib/cli/gtk-sharp-2.0/libgtksharpglue-2.so" os="!windows"/>
<dllmap dll="glibsharpglue-2" target="/usr/lib/cli/glib-sharp-2.0/libglibsharpglue-2.so" os="!windows"/>
<dllmap dll="atksharpglue-2" target="/usr/lib/cli/atk-sharp-2.0/libatksharpglue-2.so"/>
<dllmap dll="libgobject-2.0" target="libgobject-2.0.so.0" os="!windows"/>
<dllmap dll="libatk-1.0-0.dll" target="libatk-1.0.so.0" os="!windows"/>
<dllmap dll="libSkiaSharp" target="/usr/lib/cli/skiasharp/libSkiaSharp.so" os="!windows"/>
```
Now these libraries can be located by your Mono application.