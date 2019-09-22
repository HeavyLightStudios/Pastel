using Pastel.Core.Platform.Window;
using Veldrid;

namespace Pastel.Core.Platform.Graphics
{
    public partial class GraphicDevice
    {
        public GraphicsDevice Create(PastelWindow window)
        {
            var deviceOptions = new GraphicsDeviceOptions(
                false,
                PixelFormat.R16_UNorm,
                true,
                ResourceBindingModel.Improved,
                true,
                true);
            return CreateGraphicsdevice(deviceOptions, window);
        }
    }
}