using System;
using AppKit;
using CoreGraphics;

namespace Pastel.Core.Platform.Window
{
    public partial class PastelWindow
    {
        private NSWindow _window;
        public IntPtr Handle;
        public event EventHandler WindowWillClose = delegate { };

        internal void CreateWindow()
        {
            if (Fullscreen)
            {
                var frame = NSScreen.MainScreen.Frame;

                _window = new NSWindow(frame, NSWindowStyle.FullScreenWindow,
                    NSBackingStore.Buffered, false);

                _window.Level = NSWindowLevel.ScreenSaver - 1;
            }
            else
            {
                _window = new NSWindow(new CGRect(100, 100, _screenSize.Width, _screenSize.Height),
                    (NSWindowStyle.Titled | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable | NSWindowStyle.Resizable), 
                    NSBackingStore.Buffered, false);
            }

            Handle = _window.Handle;
            
            _window.Title = _title;
            _window.WillClose += internalClose;
            _window.AwakeFromNib();
            _window.Center();
            _window.MakeKeyAndOrderFront(null);
        }

        private void internalClose(object sender, EventArgs e)
        {
            WindowWillClose(this, e);
        }

        internal void ChangeScreenSize()
        {
            var frame = _window.Frame;
            frame.Size = new CGSize(_screenSize.Width, _screenSize.Height);
            
            _window.SetFrame(frame, true);
            _window.Center();
        }

        internal void ChangeFullScreen()
        {
            if (_fullscreen)
            {
                var frame = NSScreen.MainScreen.Frame;
                _window.StyleMask = NSWindowStyle.FullScreenWindow;
                _window.SetFrame(frame, true);
                
                _window.MakeMainWindow();
                
            }
            else
            {
                var frame = _window.Frame;
                frame.Size = new CGSize(_screenSize.Width, _screenSize.Height);

                _window.StyleMask = NSWindowStyle.Titled | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable;
                _window.SetFrame(frame, true);
                _window.Center();
            }
        }
    }
}