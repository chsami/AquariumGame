using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquariumGame._0
{
    public abstract class GameObject
    {
        string name;
        Texture2D texture;
        Vector2 position;
        int currentFrame;
        int totalFrames;


        private float timeBetweenFrames;
        private float time;
        

        public float TimeBetweenFrames
        {
            get { return timeBetweenFrames; }
            set { timeBetweenFrames = value; }
        }
        

        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public int CurrentFrame
        {
            get
            {
                return currentFrame;
            }
            set
            {
                currentFrame = value;
            }
        }

        public int TotalFrames
        {
            get
            {
                return totalFrames;
            }
            set
            {
                totalFrames = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        private Color objectColor;

        public Color ObjectColor
        {
            get { return objectColor; }
            set { objectColor = value; }
        }
        

        public static bool enterGameObjectWithMouse(Vector2 position, Texture2D texture, int totalFrames)
        {
            //Console.WriteLine("Mousex: " + mouseInput.getMouseX() + " is between : " + (ButtonX + ButtonTexture.Width) + " and " + ButtonX);
            // Console.WriteLine("Mousey: " + mouseInput.getMouseY() + " is between : " + (ButtonX + ButtonTexture.Height) + " and " + ButtonY);
            if (MouseInput.getMouseX() < position.X + texture.Width / totalFrames &&
                    MouseInput.getMouseX() > position.X &&
                    MouseInput.getMouseY() < position.Y + texture.Height &&
                    MouseInput.getMouseY() > position.Y)
            {
                return true;
            }
            return false;
        }

        public virtual void UpdateGameObject(GameTime gameTime)
        {
            if (currentFrame <= TotalFrames - 1 && time <= 0)
            {
                time = TimeBetweenFrames;
                CurrentFrame++;
            }
            if (CurrentFrame >= TotalFrames)
                CurrentFrame = 0;
            if (time > 0)
                time--;
        }
    }
}
