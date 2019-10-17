using System;
using Foundation;
using UIKit;

namespace Pastel.Core.Platform.Window
{
    public sealed class WindowViewController : UIViewController
    {
        public WindowViewController(IntPtr handle) : base(handle)
        {
        }

        [Export("initWithCoder:")]
        public WindowViewController(NSCoder coder) : base(coder)
        {
        }

        public WindowViewController(PastelWindow window)
        {
            // Construct the window from code here
            window.MakeKeyAndVisible();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.Green;
        }
    }
}