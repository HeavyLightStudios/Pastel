using System;
using System.Collections.Generic;
using Pastel.Core.Models;

namespace Pastel.Core.Platform.Input
{
    public partial class InputManager
    {
        private static readonly Lazy<List<Button>> _buttons = new Lazy<List<Button>>(() => new List<Button>
        {
            new Button("Left"),
            new Button("Right"),
            new Button("Up"),
            new Button("Down"),
            new Button("Action"),
            new Button("Cancel"),
            new Button("Menu"),
            new Button("Secondary"),
            new Button("LeftTop"),
            new Button("RightTop"),
            new Button("LeftTrigger"),
            new Button("RightTrigger")
        });

        public InputManager()
        {
            Initialise();
        }

        public static List<Button> Buttons => _buttons.Value;
    }
}