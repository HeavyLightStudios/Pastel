using System;
using Pastel.Models;

namespace Pastel
{
    public partial class Window
    {
        private ScreenSize _screenSize;
        private bool _fullScreen;
        
        public string Title { get; set; }

        public bool Fullscreen
        {
            get => _fullScreen;
            set
            {
                _fullScreen = value;
                ChangeFullScreen();
            }
        }

        public ScreenSize ScreenSize
        {
            get => _screenSize;
            set
            {
                _screenSize = value;
                ChangeScreenSize();
            }
        }

        public Window(ScreenSize screenSize, bool fullscreen = true, string title = "")
        {
            _screenSize = screenSize;
            _fullScreen = fullscreen;
            Title = title;
            
            CreateWindow();
        }
    }
}
