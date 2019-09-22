using Pastel.Core.Models;

namespace Pastel.Core.Platform.Window
{
    public sealed partial class PastelWindow
    {
        private readonly string _title;

        public PastelWindow()
        {
            ScreenSize = new ScreenSize(1280, 720);
            _title = "Pastel";
            Fullscreen = false;

            CreateWindow();
        }

        public PastelWindow(ScreenSize screenSize)
        {
            ScreenSize = screenSize;
            _title = "Pastel";
            Fullscreen = false;

            CreateWindow();
        }

        public PastelWindow(ScreenSize screenSize, string title)
        {
            ScreenSize = screenSize;
            _title = title;
            Fullscreen = false;

            CreateWindow();
        }

        public PastelWindow(ScreenSize screenSize, string title, bool fullscreen)
        {
            ScreenSize = screenSize;
            _title = title;
            Fullscreen = fullscreen;

            CreateWindow();
        }

        public bool Fullscreen { get; set; }

        public ScreenSize ScreenSize { get; set; }

        public void Create()
        {
            CreateWindow();
        }
    }
}