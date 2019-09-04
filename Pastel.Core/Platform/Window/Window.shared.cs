using Pastel.Core.Models;

namespace Pastel.Core.Platform.Window
{
    public partial class PastelWindow
    {
        private ScreenSize _screenSize;
        private bool _fullscreen;
        private string _title;
        public bool Running { get; set; }
        
        public bool Fullscreen
        {
            get => _fullscreen;
            set
            {
                _fullscreen = value;
                
            }
        }
        public ScreenSize ScreenSize
        {
            get => _screenSize;
            set
            {
                _screenSize = value;
                
            }
        }
        
        public PastelWindow()
        {
            _screenSize = new ScreenSize(1280, 720);
            _title = "Pastel";
            _fullscreen = false;
        }
        
        public PastelWindow(ScreenSize screenSize)
        {
            _screenSize = screenSize;
            _title = "Pastel";
            _fullscreen = false;
        }
        
        public PastelWindow(ScreenSize screenSize, string title)
        {
            _screenSize = screenSize;
            _title = title;
            _fullscreen = false;
        }

        public PastelWindow(ScreenSize screenSize, string title, bool fullscreen)
        {
            _screenSize = screenSize;
            _title = title;
            _fullscreen = fullscreen;
        }

        public void Create()
        {
            CreateWindow();
        }
    }
}