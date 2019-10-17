using System;
using Foundation;
using Pastel.Core.Models;
using UIKit;

namespace Pastel.Core.Platform.Window
{
    public sealed partial class PastelWindow : UIWindow
    {

        internal void CreateWindow()
        {
            if (ScreenSize == null)
            {
                ScreenSize = new ScreenSize(
                    (int)UIScreen.MainScreen.Bounds.Height,
                    (int)UIScreen.MainScreen.Bounds.Width
                );
            }
            else
            {
                ScreenSize.Height = (int)UIScreen.MainScreen.Bounds.Height;
                ScreenSize.Width = (int)UIScreen.MainScreen.Bounds.Width;
            }

            var windowController = new WindowViewController(Handle);

            windowController.Title = _title;
            RootViewController = windowController;
            
        }
    }
}