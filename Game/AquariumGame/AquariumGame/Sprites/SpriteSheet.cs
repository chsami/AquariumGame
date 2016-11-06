using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquariumGame._0.Sprites
{
    public static class SpriteSheet
    {

        public static Rectangle SliceSpritesheet(Texture2D texture, int currentFrame, int frames)
        {
            
            if (frames == 0)
            {
                return new Rectangle(0, 0, texture.Width, texture.Height);
            }
            return new Rectangle(texture.Width / frames * currentFrame, 0, texture.Width / frames, texture.Height);
        }
    }
}
