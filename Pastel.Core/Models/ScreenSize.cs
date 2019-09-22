namespace Pastel.Core.Models
{
    public class ScreenSize
    {
        public ScreenSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}