using System;
using AppKit;
using CoreGraphics;
using Foundation;

namespace Pastel.Core.Platform.Window
{
    public sealed partial class PastelWindow : NSWindow
    {
        public PastelWindow(IntPtr handle) : base(handle)
        {
        }

        [Export("initWithCoder:")]
        public PastelWindow(NSCoder coder) : base(coder)
        {
        }

        public PastelWindow(CGRect contentRect, NSWindowStyle aStyle, NSBackingStore bufferingType,
            bool deferCreation) :
            base(contentRect, aStyle, bufferingType, deferCreation)
        {
            ContentView = new NSView(Frame);
        }

        internal void CreateWindow()
        {
            var frame = Frame;
            frame.Size = new CGSize(ScreenSize.Width, ScreenSize.Height);

            SetFrame(frame, true);

            ContentView = new NSView(Frame);

            StyleMask = NSWindowStyle.Titled | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable |
                        NSWindowStyle.Resizable;
            BackingType = NSBackingStore.Buffered;

            Title = _title;

            var windowController = new WindowViewController(this);
            windowController.Window.Center();
            windowController.Window.MakeKeyAndOrderFront(this);
        }

        internal void ChangeScreenSize()
        {
            var frame = Frame;
            frame.Size = new CGSize(ScreenSize.Width, ScreenSize.Height);

            SetFrame(frame, true);
            Center();
        }

        internal void ChangeFullScreen()
        {
            if (Fullscreen)
            {
                var frame = NSScreen.MainScreen.Frame;
                StyleMask = NSWindowStyle.FullScreenWindow;
                SetFrame(frame, true);

                MakeMainWindow();
            }
            else
            {
                var frame = Frame;
                frame.Size = new CGSize(ScreenSize.Width, ScreenSize.Height);

                StyleMask = NSWindowStyle.Titled | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable;
                SetFrame(frame, true);
                Center();
            }
        }
    }
}