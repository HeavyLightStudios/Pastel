using System;
using UIKit;
using CoreGraphics;

namespace Pastel
{
    public partial class Window
    {
        internal void CreateWindow()
        {
            Console.WriteLine("Hello from iOS");
            var window = new UIWindow(
                new CGRect(UIScreen.MainScreen.Bounds.GetMidX(),
                    UIScreen.MainScreen.Bounds.GetMidY(), 500, 500), 
                NSWindowStyle.Borderless, NSBackingStore.Buffered, false);

            window.Title = "Test Window";
            window.IsOpaque = false;
            window.Center();
            window.MovableByWindowBackground = true;
            window.BackgroundColor = NSColor.Green;
            window.MakeKeyAndOrderFront(null);
        }
    }
}
