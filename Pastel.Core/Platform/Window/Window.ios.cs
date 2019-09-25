using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Pastel.Core.Platform.Window
{
    public sealed partial class PastelWindow : UIWindow
    {
        public PastelWindow(IntPtr handle) : base(handle)
        {
        }

        [Export("initWithCoder:")]
        public PastelWindow(NSCoder coder) : base(coder)
        {
        }

        internal void CreateWindow()
        {
            ScreenSize.Height = (int) UIScreen.MainScreen.Bounds.Height;
            ScreenSize.Width = (int) UIScreen.MainScreen.Bounds.Width;

            var windowController = new WindowViewController(this);

            windowController.Title = _title;
            RootViewController = windowController;
            MakeKeyAndVisible();
        }
    }
}