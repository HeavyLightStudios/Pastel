using AppKit;

namespace Pastel.Core.Platform.Input
{
    public partial class InputManager
    {
        protected void Initialise()
        {
            NSEvent.AddLocalMonitorForEventsMatchingMask(NSEventMask.KeyDown, (NSEvent Event) =>
            {
                switch(Event.Characters)
                {
                    case "w":
                        Buttons.Find(b => b.Name == "Up").pressed = true;
                        return null;
                    case "s":
                        Buttons.Find(b => b.Name == "Down").pressed = true;
                        return null;
                    case "a":
                        Buttons.Find(b => b.Name == "Left").pressed = true;
                        return null;
                    case "d":
                        Buttons.Find(b => b.Name == "Right").pressed = true;
                        return null;
                }
                return Event;
            });

            NSEvent.AddLocalMonitorForEventsMatchingMask(NSEventMask.KeyUp, (NSEvent Event) =>
            {
                switch (Event.Characters)
                {
                    case "w":
                        Buttons.Find(b => b.Name == "Up").pressed = false;
                        return null;
                    case "s":
                        Buttons.Find(b => b.Name == "Down").pressed = false;
                        return null;
                    case "a":
                        Buttons.Find(b => b.Name == "Left").pressed = false;
                        return null;
                    case "d":
                        Buttons.Find(b => b.Name == "Right").pressed = false;
                        return null;
                }
                return Event;
            });
        }
    }
}
