using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquariumGame._0.Particles
{
    public class Particle
    {
        string name;
        private Texture2D texture;

        public const float MIN_SPEED = 0.01f;
        public const float MAX_SPEED = 0.05f;

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

        private Vector2 movingPosition;

        public Vector2 MovingPosition
        {
            get { return movingPosition; }
            set { movingPosition = value; }
        }

        private float timeAlive;

        public float TimeAlive
        {
            get { return timeAlive; }
            set { timeAlive = value; }
        }

        private int size;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }
        
        

        public Particle(string name, Texture2D texture, Vector2 position)
        {
            this.name = name;
            this.texture = texture;
            this.position = position;
            MovingPosition = Vector2.Zero;
            Size = 1;
        }
    }
}
