using System;
using System.Collections.Generic;
using Pastel.Core.Models;

namespace Pastel.Core.Platform.Input
{
    public partial class InputManager
    {
        private static readonly Lazy<List<Button>> _buttons = new Lazy<List<Button>>(() => new List<Button>
        {
            new Button {Name = "Left"},
            new Button {Name = "Right"},
            new Button {Name = "Up"},
            new Button {Name = "Down"},
            new Button {Name = "Action"},
            new Button {Name = "Cancel"},
            new Button {Name = "Menu"},
            new Button {Name = "Secondary"},
            new Button {Name = "LeftTop"},
            new Button {Name = "RightTop"},
            new Button {Name = "LeftTrigger"},
            new Button {Name = "RightTrigger"}
        });

        public InputManager()
        {
            Initialise();
        }

        public static List<Button> Buttons => _buttons.Value;
    }
}