using Veldrid;
using Pastel.Core.Platform.Window;

namespace Pastel.Core.Platform.Graphics
{
    public partial class GraphicDevice
    {
        public GraphicsDevice Create(PastelWindow window)
        {
            GraphicsDeviceOptions deviceOptions = new GraphicsDeviceOptions(
                debug: false,
                swapchainDepthFormat: PixelFormat.R16_UNorm,
                syncToVerticalBlank: true,
                resourceBindingModel: ResourceBindingModel.Improved,
                preferDepthRangeZeroToOne: true,
                preferStandardClipSpaceYDirection: true);
            return CreateGraphicsdevice(deviceOptions, window);
        }
    }
}