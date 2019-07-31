using System;
using AppKit;
using CoreGraphics;

namespace Pastel
{
    public partial class Window
    {
        internal void CreateWindow()
        {
            var window = new NSWindow(new CGRect(
                    NSScreen.MainScreen.Frame.GetMidX(),
                    NSScreen.MainScreen.Frame.GetMidY(),1000,500), 
                (NSWindowStyle.Titled | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable | NSWindowStyle.Resizable), 
                NSBackingStore.Buffered, false);
            
            
            window.AwakeFromNib();
            window.Center();
            window.MakeKeyAndOrderFront(null);
            
            Console.WriteLine("Hello from MacOS");
        }
    }
}
