using Pastel.Core.Platform.Window;
using Veldrid;

namespace Pastel.Core.Platform.Graphics
{
    public partial class GraphicDevice
    {
        internal GraphicsDevice CreateGraphicsdevice(GraphicsDeviceOptions options, PastelWindow window)
        {
            var swapChainSource = getSwapchainSource(window);
            var swapchainDescription = new SwapchainDescription(
                swapChainSource,
                (uint) window.ScreenSize.Width, (uint) window.ScreenSize.Height,
                options.SwapchainDepthFormat,
                options.SyncToVerticalBlank,
                false);
            
            return GraphicsDevice.CreateMetal(options, swapchainDescription);
            //TODO: Add OpenGL Graphic Device & check GraphicDevice.IsBackendSupported
        }
        
        private SwapchainSource getSwapchainSource(PastelWindow pastelWindow)
        {
            return SwapchainSource.CreateUIView(pastelWindow.Handle);
        }

    }
}