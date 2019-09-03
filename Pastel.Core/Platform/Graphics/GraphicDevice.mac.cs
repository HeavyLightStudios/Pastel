using System;
using System.Runtime.InteropServices;
using Pastel.Core.Window;
using Veldrid;

namespace Pastel.Core.Platform.Graphics
{
    public partial class GraphicDevice
    {
        internal unsafe GraphicsDevice CreateGraphicsdevice(GraphicsDeviceOptions options, PastelWindow window)
        {
            var swapChainSource = getSwapchainSource(window);
            var swapchainDesc = new SwapchainDescription(
                swapChainSource,
                (uint)window.ScreenSize.Width, (uint)window.ScreenSize.Height,
                options.SwapchainDepthFormat,
                options.SyncToVerticalBlank,
                false);
            
            return GraphicsDevice.CreateMetal(options, swapchainDesc);
        }

        internal unsafe SwapchainSource getSwapchainSource(PastelWindow pastelWindow)
        {
            return SwapchainSource.CreateNSWindow(pastelWindow.windowHandle);
        }
    }
}