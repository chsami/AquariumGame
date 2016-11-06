using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AquariumGame._0.Config;
using AquariumGame._0.Screens;
using AquariumGame._0.GameInterfaces;
namespace AquariumGame._0.Input
{
    class KeyBoardInput
    {
        static KeyboardState oldState;
        static bool stopCheck = false;
        public static void handleOverallInput() //get updated 16.6 times every 1 second
        {
            KeyboardState key = new KeyboardState();
            Keys[] multipleKeys;
            key = Keyboard.GetState();
            multipleKeys = key.GetPressedKeys();
            handleChatBoxInput(multipleKeys);
        }

        public static void handleChatBoxInput(Keys[] keys)
        {
            const float factor = 63f / 64f;
            KeyboardState newState = Keyboard.GetState();
            foreach (Keys key in keys)
            {
                if (newState.IsKeyDown(key))
                {
                    if (!oldState.IsKeyDown(key))
                    {
                        foreach (TextBox item in ScreenManager.textboxes)
                        {
                            if (item.handleText(key))
                                stopCheck = true;
                        }
                        if (!stopCheck)
                        {
                            switch (key)
                            {
                                case Keys.NumPad1:
                                    WorldConfigurator.developerMode = WorldConfigurator.developerMode == true ? false : true;
                                    break;
                                case Keys.Q:
                                    Screens.ScreenManager.water.Tension *= factor;
                                    break;
                                case Keys.Z:
                                    Screens.ScreenManager.water.Tension /= factor;
                                    break;
                                case Keys.E:
                                    Screens.ScreenManager.water.Dampening *= factor;
                                    break;
                                case Keys.R:
                                    Screens.ScreenManager.water.Dampening /= factor;
                                    break;
                                case Keys.T:
                                    Screens.ScreenManager.water.Spread *= factor;
                                    break;
                                case Keys.Y:
                                    Screens.ScreenManager.water.Spread /= factor;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            oldState = newState;
            stopCheck = false;
        }
    }
}
