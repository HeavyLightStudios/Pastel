using System;
using System.Threading;
using AppKit;
using CoreGraphics;
using Foundation;
using Pastel.Core.Models;

namespace Pastel.Core.Platform.Window
{
    public class WindowViewController: NSWindowController
    {
        public WindowViewController(IntPtr handle) : base(handle)
        {
        }
        
        [Export ("initWithCoder:")]
        public WindowViewController (NSCoder coder) : base (coder)
        {
        }

        public WindowViewController (PastelWindow window, ScreenSize screenSize) : base ("MainWindow")
        {
            // Construct the window from code here
            base.Window = window;

            // Simulate Awaking from Nib
            Window.AwakeFromNib ();
        }

        public override void AwakeFromNib ()
        {
            base.AwakeFromNib ();
        }

        public override void WindowDidLoad()
        {
            Console.WriteLine("Here I am");
        }
    }
}