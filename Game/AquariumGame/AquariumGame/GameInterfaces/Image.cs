using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AquariumGame._0.GameLevels;

namespace AquariumGame._0.GameInterfaces
{
    public class Image : Components
    {

        public Image(string name, Texture2D texture, Vector2 position, bool clickable, string hoverableText, Texture2D hoverableTexture, bool visible, bool changeSize)
        {
            Name = name;
            Texture = texture;
            OriginalTexture = texture;
            TextureWidth = texture.Width;
            TextureHeight = texture.Height;
            Position = position;
            Clickable = clickable;
            HoverableText = hoverableText;
            HoverableTexture = hoverableTexture;
            Visible = visible;
            ComponentColor = Color.White;
            Animated = changeSize;
            
            if (changeSize)
                TextureWidth = 0;
        }

        public Image(string name, Texture2D texture, Vector2 position, bool clickable, string hoverableText, Texture2D hoverableTexture, bool visible, Point point)
        {
            Name = name;
            Texture = texture;
            OriginalTexture = texture;
            TextureWidth = point.X;
            TextureHeight = point.Y;
            Position = position;
            Clickable = clickable;
            HoverableText = hoverableText;
            HoverableTexture = hoverableTexture;
            Visible = visible;
            ComponentColor = Color.White;
        }

        public override void DrawComponent()
        {
            if (Name.Equals("loadbar_empty_1") && Visible)
            {
                if (Level.percentageTillNextLevel > 20)
                {
                    ComponentColor = new Color((int)Level.percentageTillNextLevel, (int)Level.percentageTillNextLevel, (int)Level.percentageTillNextLevel);
                    Screens.ScreenManager.Sprites.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y,
                        TextureWidth = (int)Level.percentageTillNextLevel > 599 ? TextureWidth : (int)Level.percentageTillNextLevel, TextureHeight), ComponentColor);
                }
            } else
            if (Visible)
                Screens.ScreenManager.Sprites.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, TextureWidth, TextureHeight), ComponentColor);
            
        }
    }
}
