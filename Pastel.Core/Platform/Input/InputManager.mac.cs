using System;
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
                        break;
                    case "s":
                        Buttons.Find(b => b.Name == "Down").pressed = true;
                        break;
                    case "a":
                        Buttons.Find(b => b.Name == "Left").pressed = true;
                        break;
                    case "d":
                        Buttons.Find(b => b.Name == "Right").pressed = true;
                        break;
                }
                return Event;
            });

            NSEvent.AddLocalMonitorForEventsMatchingMask(NSEventMask.KeyUp, (NSEvent Event) =>
            {
                switch (Event.Characters)
                {
                    case "w":
                        Buttons.Find(b => b.Name == "Up").pressed = false;
                        break;
                    case "s":
                        Buttons.Find(b => b.Name == "Down").pressed = false;
                        break;
                    case "a":
                        Buttons.Find(b => b.Name == "Left").pressed = false;
                        break;
                    case "d":
                        Buttons.Find(b => b.Name == "Right").pressed = false;
                        break;
                }
                return Event;
            });
        }
    }
}
