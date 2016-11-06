using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquariumGame._0.Particles
{
    class ParticleManager
    {
        Particle[] bubbles = new Particle[100];
        Texture2D bubbleTexture;
        public void LoadContent()
        {
            bubbleTexture = Screens.ScreenManager.ContentMgr.Load<Texture2D>(Screens.ScreenManager.SPRITE_PARTICLES + "buuble");
            for (int i = 0; i < bubbles.Length; i++)
            {
                bubbles[i] = new Particle("bubbles", bubbleTexture, new Vector2((float)Misc.generaterValue(100, 1, Screens.ScreenManager.DEFAULT_SCREEN_WIDTH), 700f));
            }
            Console.WriteLine("Succesfully loaded content.");
        }

        public void UnloadContent()
        {
            for (int i = 0; i < bubbles.Length; i++)
            {
                bubbles[i] = null;
            }
            Console.WriteLine("Succesfully unloaded content.");
        }

        public void Update(GameTime gameTime)
        {
            foreach (var item in bubbles)
            {
                if (Misc.generaterValue(1, 1, 10000) == 2)
                {
                    if (item.Position.Y == 700f)
                    {
                        item.MovingPosition = new Vector2(item.Position.X, (float)Misc.generaterValue(1, 1, 100));
                        item.Size = Misc.generaterValue(1, 1, 4);
                    }
                }
                if (item.MovingPosition != Vector2.Zero)
                {
                    if (item.Position.Y >= item.MovingPosition.Y)
                    {
                        item.Position = new Vector2(item.Position.X, item.Position.Y - (float)Particle.MAX_SPEED * (float)gameTime.ElapsedGameTime.Milliseconds);
                    }
                    if (item.Position.Y < item.MovingPosition.Y)
                    {
                        item.Position = new Vector2((float)Misc.generaterValue(50, 1, Screens.ScreenManager.DEFAULT_SCREEN_WIDTH), 700f);
                        item.MovingPosition = Vector2.Zero;
                    }
                }
            }
        }

        public void Draw()
        {
            foreach (var item in bubbles)
            {
                if (item != null)
                    Screens.ScreenManager.Sprites.Draw(item.Texture, new Rectangle((int)item.Position.X, (int)item.Position.Y, item.Texture.Width / item.Size, item.Texture.Height / item.Size), Color.White);
            }
        }
    }
}
