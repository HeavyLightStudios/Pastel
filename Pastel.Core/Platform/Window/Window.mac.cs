using System;
using System.Threading;
using AppKit;
using CoreGraphics;
using Foundation;
using Pastel.Core.Models;

namespace Pastel.Core.Platform.Window
{
    public partial class PastelWindow: NSWindow
    {
        public PastelWindow(IntPtr handle) : base (handle)
        {
        }
        
        [Export ("initWithCoder:")]
        public PastelWindow (NSCoder coder) : base (coder)
        {
        }

        public PastelWindow(CGRect contentRect, NSWindowStyle aStyle, NSBackingStore bufferingType, bool deferCreation) :
            base(contentRect, aStyle, bufferingType, deferCreation)
        {
            ContentView = new NSView(Frame);
        }

        internal void CreateWindow()
        {
            var frame = Frame;
            frame.Size = new CGSize(_screenSize.Width, _screenSize.Height);
            
            SetFrame(frame, true);
            
            ContentView = new NSView(Frame);

            StyleMask = NSWindowStyle.Titled | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable |
                        NSWindowStyle.Resizable;
            BackingType = NSBackingStore.Buffered;

            Title = _title;
            
            var windowController = new WindowViewController(this, _screenSize);
            windowController.Window.Center();
            windowController.Window.MakeKeyAndOrderFront (this);
        }

        public override void MakeKeyAndOrderFront(NSObject sender)
        {
            base.MakeKeyAndOrderFront(sender);
            Console.WriteLine("Here");
        }

        internal void ChangeScreenSize()
        {
            var frame = Frame;
            frame.Size = new CGSize(_screenSize.Width, _screenSize.Height);
            
            SetFrame(frame, true);
            Center();
        }

        internal void ChangeFullScreen()
        {
            if (_fullscreen)
            {
                var frame = NSScreen.MainScreen.Frame;
                StyleMask = NSWindowStyle.FullScreenWindow;
                SetFrame(frame, true);
                
                MakeMainWindow();
                
            }
            else
            {
                var frame = Frame;
                frame.Size = new CGSize(_screenSize.Width, _screenSize.Height);

                StyleMask = NSWindowStyle.Titled | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable;
                SetFrame(frame, true);
                Center();
            }
        }
    }
}