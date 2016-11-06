using AquariumGame._0.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquariumGame._0
{
    public class Food : GameObject
    {

        float foodRotation;

        public float FoodRotation
        {
            get
            {
                return foodRotation;
            }
            set
            {
                foodRotation = value;
            }
        }

        private Vector2 velocity;

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        private Vector2 gravity = new Vector2(0f, 0.1f);

        
        

        public Food(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            foodRotation = 0.1f;
            ObjectColor = Color.White;
        }

        float timeSinceLastFoodMove = 0f;

        public void Update(GameTime gameTime)
        {
            if (Position.Y > ScreenManager.water.GetHeight(Position.X))
                Velocity *= 0.84f;
            if (Position.Y < 100 && Position.Y + Velocity.Y >= 100)
                ScreenManager.water.Splash(Position.X, Velocity.Y * Velocity.Y * 5);
            if (Position.Y < ScreenManager.GraphicsDeviceMgr.PreferredBackBufferHeight - Texture.Height)
            {
                timeSinceLastFoodMove += 0.0005f * (float) gameTime.ElapsedGameTime.Milliseconds;
                Position = new Vector2(Position.X, Position.Y + timeSinceLastFoodMove);
                FoodRotation += (float) gameTime.ElapsedGameTime.TotalSeconds;
                Position += Velocity;
                Velocity += gravity;
            }
            else
            {
                try
                {
                    ScreenManager.food.Remove(this);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Draw()
        {
            Screens.ScreenManager.Sprites.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, Texture.Width / 2, Texture.Height / 2), null, ObjectColor, FoodRotation, new Vector2(0, 0), SpriteEffects.None, 0f);
        }

    }
}
