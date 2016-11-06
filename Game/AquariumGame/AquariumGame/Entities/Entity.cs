using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AquariumGame._0.Screens;

namespace AquariumGame._0.Entities
{
    public abstract class Entity
    {
        private string name;

        public string Name
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

        private int health;

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        private int size = 1;

        public int Size
        {
            get { return size; }
            set
            {
                if (value == 0)
                {
                    size = 1;
                }
                else
                {
                    size = value;
                } 
            }
        }

        private Color entColor;

        public Color EntColor
        {
            get { return entColor; }
            set { entColor = value; }
        }
        
        
        
        
 

        private float currentMovementSpeed;

        public float CurrentMovementSpeed
        {
            get { return currentMovementSpeed; }
            set { currentMovementSpeed = value; }
        }
        

        public abstract void UpdateEntity(GameTime gameTime);

        public virtual void DrawEntity()
        {
            ScreenManager.Sprites.Draw(texture, new Rectangle((int)position.X, (int)position.Y, texture.Width / size, texture.Height / size), entColor);
        }
    }
}
