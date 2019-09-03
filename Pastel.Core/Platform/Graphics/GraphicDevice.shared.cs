using Veldrid;
using Pastel.Core.Platform.Window;

namespace Pastel.Core.Platform.Graphics
{
    public partial class GraphicDevice
    {
        public GraphicsDevice Create(PastelWindow window)
        {
            return CreateGraphicsdevice(new GraphicsDeviceOptions(), window);
        }
    }
}