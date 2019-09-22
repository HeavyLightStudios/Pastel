using AppKit;

namespace Pastel.Core.Platform.Input
{
    public partial class InputManager
    {
        protected void Initialise()
        {
            NSEvent.AddLocalMonitorForEventsMatchingMask(NSEventMask.KeyDown, Event =>
            {
                switch (Event.Characters)
                {
                    case "w":
                        Buttons.Find(b => b.Name == "Up").Pressed = true;
                        return null;
                    case "s":
                        Buttons.Find(b => b.Name == "Down").Pressed = true;
                        return null;
                    case "a":
                        Buttons.Find(b => b.Name == "Left").Pressed = true;
                        return null;
                    case "d":
                        Buttons.Find(b => b.Name == "Right").Pressed = true;
                        return null;
                }

                return Event;
            });

            NSEvent.AddLocalMonitorForEventsMatchingMask(NSEventMask.KeyUp, Event =>
            {
                switch (Event.Characters)
                {
                    case "w":
                        Buttons.Find(b => b.Name == "Up").Pressed = false;
                        return null;
                    case "s":
                        Buttons.Find(b => b.Name == "Down").Pressed = false;
                        return null;
                    case "a":
                        Buttons.Find(b => b.Name == "Left").Pressed = false;
                        return null;
                    case "d":
                        Buttons.Find(b => b.Name == "Right").Pressed = false;
                        return null;
                }

                return Event;
            });
        }
    }
}