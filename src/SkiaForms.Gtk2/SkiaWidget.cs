/*
Taken from SkiaSharp https://github.com/mono/SkiaSharp/blob/master/source/SkiaSharp.Views/SkiaSharp.Views.Gtk/SKWidget.cs (3b1e7a0 on 24 Mar 2018)

Copyright (c) 2015-2016 Xamarin, Inc.
Copyright (c) 2017-2018 Microsoft Corporation.

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.ComponentModel;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace SkiaForms.Gtk2
{
    [ToolboxItem(true)]
    public class SkiaWidget : global::Gtk.DrawingArea
    {
        private Gdk.Pixbuf pix;

        public SkiaWidget()
        {
            DoubleBuffered = false;
        }

        public SKSize CanvasSize => pix == null ? SKSize.Empty : new SKSize(pix.Width, pix.Height);

        [Category("Appearance")]
        public event EventHandler<SKPaintSurfaceEventArgs> PaintSurface;

        protected override bool OnExposeEvent(Gdk.EventExpose evnt)
        {
            var result = base.OnExposeEvent(evnt);

            var window = evnt.Window;
            var area = evnt.Area;

            // get the pixbuf
            CreatePixbuf();
            var info = new SKImageInfo(pix.Width, pix.Height, SKImageInfo.PlatformColorType, SKAlphaType.Premul);

            // create the surface
            using (var surface = SKSurface.Create(info, pix.Pixels, info.RowBytes))
            {
                // start drawing
                OnPaintSurface(new SKPaintSurfaceEventArgs(surface, info));

                surface.Canvas.Flush();

                // swap R and B
                if (info.ColorType == SKColorType.Bgra8888)
                {
                    using (var pixmap = surface.PeekPixels())
                    {
                        SKSwizzle.SwapRedBlue(pixmap.GetPixels(), info.Width * info.Height);
                    }
                }
            }

            // write the pixbuf to the graphics
            window.Clear();
            window.DrawPixbuf(null, pix, 0, 0, 0, 0, -1, -1, Gdk.RgbDither.None, 0, 0);

            return result;
        }

        protected virtual void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            // invoke the event
            PaintSurface?.Invoke(this, e);
        }

        ~SkiaWidget()
        {
            Dispose(false);
        }

        public override void Destroy()
        {
            GC.SuppressFinalize(this);
            Dispose(true);

            base.Destroy();
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);

            base.Dispose();
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                FreePixbuf();
            }
        }

        private void CreatePixbuf()
        {
            var alloc = Allocation;
            var w = alloc.Width;
            var h = alloc.Height;
            if (pix == null || pix.Width != w || pix.Height != h)
            {
                FreePixbuf();

                pix = new Gdk.Pixbuf(Gdk.Colorspace.Rgb, true, 8, w, h);
            }
        }

        private void FreePixbuf()
        {
            if (pix != null)
            {
                pix.Dispose();
                pix = null;
            }
        }
    }
}