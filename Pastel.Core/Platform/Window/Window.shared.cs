using System;
using System.Dynamic;
using Pastel.Core.Models;

namespace Pastel.Core.Window
{
    public partial class PastelWindow
    {
        private ScreenSize _screenSize;
        private bool _fullscreen;
        private string _title;
        public bool Visibile { get; set; }
        
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
            
        }

        public void Create()
        {
            _screenSize = new ScreenSize(500, 500);
            _title = "Pastel";
            _fullscreen = false;
            CreateWindow();
        }
    }
}