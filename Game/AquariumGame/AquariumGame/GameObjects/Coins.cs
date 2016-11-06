using AquariumGame._0.Screens;
using AquariumGame._0.User;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquariumGame._0
{
    public class Coins : GameObject
    {

        public const float COIN_MOVEMENT = 0.15f;
        private int moneyBuffer = 0;

        public Coins(string name, Texture2D texture, Vector2 position, int totalFrames, float timeBetweenFrames)
        {
            Name = name;
            Texture = texture;
            Position = position;
            CurrentFrame = 0;
            TotalFrames = totalFrames;
            TimeBetweenFrames = timeBetweenFrames;
        }

        public void Update(GameTime gameTime)
        {
            if (ScreenManager.enemies.Count == 0)
            {
                if (MouseInput.LastMouseState.RightButton == ButtonState.Released && MouseInput.MouseState.RightButton == ButtonState.Pressed)
                {
                    if (GameObject.enterGameObjectWithMouse(Position, Texture, TotalFrames))
                    {
                        ScreenManager.coins.Remove(this);
                        Player.MoneyBuffer += 1250;
                    }
                }
            }
                if (Position.Y < ScreenManager.GraphicsDeviceMgr.PreferredBackBufferHeight - Texture.Height)
                    Position = new Vector2(Position.X, Position.Y + COIN_MOVEMENT * (float)gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void Draw()
        {
            Screens.ScreenManager.Sprites.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, Texture.Width / TotalFrames,
                Texture.Height), Sprites.SpriteSheet.SliceSpritesheet(Texture, CurrentFrame, TotalFrames), Color.White, 0f,
                new Vector2(0, 0), SpriteEffects.None, 0f);    
        }

    }
}
