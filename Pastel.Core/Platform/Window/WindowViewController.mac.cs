using System;
using AppKit;
using Foundation;

namespace Pastel.Core.Platform.Window
{
    public sealed class WindowViewController : NSWindowController
    {
        public WindowViewController(IntPtr handle) : base(handle)
        {
        }

        [Export("initWithCoder:")]
        public WindowViewController(NSCoder coder) : base(coder)
        {
        }

        public WindowViewController(PastelWindow window) : base("MainWindow")
        {
            // Construct the window from code here
            Window = window;

            // Simulate Awaking from Nib
            Window.AwakeFromNib();
        }
    }
}