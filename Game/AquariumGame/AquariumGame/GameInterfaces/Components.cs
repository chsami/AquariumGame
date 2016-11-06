using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquariumGame._0.GameInterfaces
{
    public abstract class Components
    {

        private string  name;

        public string  Name
        {
            get { return name; }
            set { name = value; }
        }

        private Texture2D texture;

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private bool clickable;

        public bool Clickable
        {
            get { return clickable; }
            set { clickable = value; }
        }

        private string hoverableText;

        public string HoverableText
        {
            get { return hoverableText; }
            set { hoverableText = value; }
        }

        private Texture2D hoverableTexture;

        public Texture2D HoverableTexture
        {
            get { return hoverableTexture; }
            set { hoverableTexture = value; }
        }

        private bool visible;

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        private int textureWidth;

        public int TextureWidth
        {
            get { return textureWidth; }
            set { textureWidth = value; }
        }

        private int textureHeight;

        public int TextureHeight
        {
            get { return textureHeight; }
            set { textureHeight = value; }
        }

        private bool animated;

        public bool Animated
        {
            get { return animated; }
            set { animated = value; }
        }

        private Texture2D originalTexture;

        public Texture2D OriginalTexture
        {
            get { return originalTexture; }
            set { originalTexture = value; }
        }

        private Color componentColor;

        public Color ComponentColor
        {
            get { return componentColor; }
            set { componentColor = value; }
        }
        
        
        

        public bool enterComponent()
        {
            if (MouseInput.getMouseX() < Position.X + TextureWidth &&
                    MouseInput.getMouseX() > Position.X &&
                    MouseInput.getMouseY() < Position.Y + TextureHeight &&
                    MouseInput.getMouseY() > Position.Y && Visible)
            {
                return true;
            }
            return false;
        }

        public bool clickComponent()
        {
            if (enterComponent() && MouseInput.LastMouseState.LeftButton == ButtonState.Pressed && MouseInput.MouseState.LeftButton == ButtonState.Released) 
            {
                return true;
            }
            return false;
        }

        public void enterHoverableComponent()
        {
            if (enterComponent() && Visible)
            {
                Texture = HoverableTexture;
            }
            else if (!enterComponent() && Visible && Texture == HoverableTexture)
            {
                Texture = OriginalTexture;
            }
        }


        public virtual void UpdateComponent()
        {
            if (HoverableTexture != null)
                enterHoverableComponent();
            if (animated)
            {
                textureWidth = textureWidth >= Texture.Width ? Texture.Width : textureWidth += 3;
                textureHeight = textureHeight >= Texture.Height ? Texture.Height : textureHeight += 3;
            }
        }

        public virtual void DrawComponent()
        {
            if (Visible)
                Screens.ScreenManager.Sprites.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, textureWidth, textureHeight), ComponentColor);
        }
    }
}
