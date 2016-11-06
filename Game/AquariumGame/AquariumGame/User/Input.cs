using AquariumGame._0.Entities;
using AquariumGame._0.Screens;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquariumGame._0.User
{
    static class Actions
    {
        static bool[] displayFood= {false};
        static bool readyToGo = false;
        public static void clickForFood()
        {
            if (readyToGo)
                readyToGo = false;
            foreach (var button in ScreenManager.buttons)
            {
                if (readyToGo == false)
                    readyToGo = button.enterComponent();
            }
            if (MouseInput.LastMouseState.LeftButton == ButtonState.Released && MouseInput.MouseState.LeftButton == ButtonState.Pressed && !readyToGo && ScreenManager.enemies.Count == 0)
            {
                displayFood[0] = true;
            }
            else
            {
                displayFood[0] = false;
            }
            if (displayFood[0])
            {
                ScreenManager.addFood();
                displayFood[0] = false;
            }
        }
        public static void hitEnemy()
        {
            if (MouseInput.LastMouseState.LeftButton == ButtonState.Released && MouseInput.MouseState.LeftButton == ButtonState.Pressed)
            {
                foreach (Enemy enemy in ScreenManager.enemies)
                {
                    if (MouseInput.getMouseX() < enemy.Position.X + enemy.Texture.Width &&
                         MouseInput.getMouseX() > enemy.Position.X &&
                         MouseInput.getMouseY() < enemy.Position.Y + enemy.Texture.Height &&
                         MouseInput.getMouseY() > enemy.Position.Y)
                    {
                        Entities.Enemy.ReceiveDamage = true;
                    }
                }
            }
        }
         
    }
}
