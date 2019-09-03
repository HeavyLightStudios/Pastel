using Pastel.Core.Window;
using Veldrid;

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