using System;
using Pastel.Models;

namespace Pastel
{
    public partial class Window
    {
        private ScreenSize _screenSize;
        
        public string Title { get; set; }
        public bool Fullscreen { get; set; }

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
            Fullscreen = fullscreen;
            Title = title;
            
            CreateWindow();
        }
    }
}
