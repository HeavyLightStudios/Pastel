using System;
using Foundation;
using GameController;

namespace Pastel.Core.Platform.Input
{
    public partial class InputManager
    {
        private NSObject? didConnectObserver;
        private NSObject? didDisconnectObserver;

        protected void Initialise()
        {
            ConfigureGameControllers();
        }

        public void ConfigureGameControllers()
        {
            // Receive notifications when a controller connects or disconnects.
            didConnectObserver = GCController.Notifications.ObserveDidConnect(GameControllerDidConnect);
            didDisconnectObserver = GCController.Notifications.ObserveDidDisconnect(GameControllerDidDisconnect);

            // Configure all the currently connected game controllers.
            ConfigureConnectedGameControllers();

            // And start looking for any wireless controllers.
            GCController.StartWirelessControllerDiscovery(() => Console.WriteLine("Finished finding controllers"));
        }

        private void ConfigureConnectedGameControllers()
        {
            if (GCController.Controllers == null)
                return;

            foreach (var controller in GCController.Controllers)
            {
                Console.WriteLine(controller.PlayerIndex);
            }

        }

        private void GameControllerDidDisconnect(object sender, NSNotificationEventArgs e)
        {
            var controller = (GCController)e.Notification.Object;
            Console.WriteLine("Disconnected game controller: {0}", controller);

            var playerIndex = controller.PlayerIndex;
        }

        private void GameControllerDidConnect(object sender, NSNotificationEventArgs e)
        {
            GCControllerDirectionPadValueChangedHandler dpadMoveHandler = (dpad, xValue, yValue) => {
                if(dpad.Up.IsPressed)
                    Buttons.Find(b => b.Name == "Up").Pressed = true;
                else
                    Buttons.Find(b => b.Name == "Up").Pressed = false;

                if (dpad.Down.IsPressed)
                    Buttons.Find(b => b.Name == "Down").Pressed = true;
                else
                    Buttons.Find(b => b.Name == "Down").Pressed = false;

                if (dpad.Left.IsPressed)
                    Buttons.Find(b => b.Name == "Left").Pressed = true;
                else
                    Buttons.Find(b => b.Name == "Left").Pressed = false;

                if (dpad.Right.IsPressed)
                    Buttons.Find(b => b.Name == "Right").Pressed = true;
                else
                    Buttons.Find(b => b.Name == "Right").Pressed = false;
            };

            var controller = (GCController)e.Notification.Object;
            Console.WriteLine("Connected game controller: {0}", controller);


            controller.ExtendedGamepad.DPad.ValueChangedHandler = dpadMoveHandler;
            

            var playerIndex = controller.PlayerIndex;
        }
    }
}