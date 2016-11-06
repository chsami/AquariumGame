using AquariumGame._0.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquariumGame._0.Entities
{
    public class Friendly : Entity
    {
        const float HUNGER_MODIFIER = 0.008f;
        const float FISH_MOVEMENT = 0.05f;
        const float FRAMERATE_UPDATE = 16.6f;
        const float EAT_MODIFIER = 50f;
        const float HUNGER_RATE = 100;//when the fish will get hungry
        private List<int> pillList = new List<int>();

        private int dropRate;

        public int DropRate
        {
            get { return dropRate; }
            set { dropRate = value; }
        }

        private Vector2 wanderingPoint;

        public Vector2 WanderingPoint
        {
            get { return wanderingPoint; }
            set { wanderingPoint = value; }
        }

        private float hungerLevel;

        public float HungerLevel
        {
            get { return hungerLevel; }
            set { hungerLevel = value; }
        }
        
        public Friendly(string name, Texture2D texture, Vector2 position, int health, int size, int dropRate, int hungerLevel)
        {
            Name = name;
            Texture = texture;
            Position = position;
            Health = health;
            Size = size;
            this.HungerLevel = hungerLevel;
            this.DropRate = dropRate;
            EntColor = Color.White;
            CurrentMovementSpeed = 1.5f;
            WanderingPoint = Vector2.Zero;
        }



        public override void UpdateEntity(GameTime gameTime)
        {
            if (hungerLevel < 100)
            {
                EntColor = Color.Aqua;
            }
            else
            {
                EntColor = Color.White;
            }
            if (ScreenManager.coins.Count < 200)
            {
                if (HungerLevel > HUNGER_RATE)
                {
                    
                    if (Misc.generaterValue(1, 1, DropRate) == 2)
                    {
                        ScreenManager.addCoin("golden_coin_" + gameTime.TotalGameTime, "golden_coin", new Vector2(Position.X, Position.Y), 10, Coins.COIN_MOVEMENT * (float)gameTime.ElapsedGameTime.TotalMilliseconds);
                    }
                }
                if (HungerLevel <= 0 || Health <= 0)
                {
                    ScreenManager.friendly.Remove(this);
                }
            }
            HungerLevel -= HUNGER_MODIFIER * (float)gameTime.ElapsedGameTime.TotalMilliseconds;//every 32 sec need feed or fish dies
            
            if (WanderingPoint == Vector2.Zero)
            {
                WanderingPoint = new Vector2(Misc.generaterValue(1, 50, ScreenManager.GraphicsDeviceMgr.PreferredBackBufferWidth - Texture.Width), Misc.generaterValue(1, (int) ScreenManager.water.GetHeight(Position.X), ScreenManager.GraphicsDeviceMgr.PreferredBackBufferHeight - Texture.Height));
                if ((float)gameTime.ElapsedGameTime.TotalMilliseconds > 0)
                    CurrentMovementSpeed = FISH_MOVEMENT * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            foreach (var pill in ScreenManager.food.ToList())
            {
                if (HungerLevel < HUNGER_RATE)
                {
                    if (new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height).Intersects(new Rectangle((int)pill.Position.X, (int)pill.Position.Y, (int)(pill.Texture.Width / Size), (int)(pill.Texture.Height / Size))))
                    {
                        HungerLevel = HungerLevel < 255 ? HungerLevel + EAT_MODIFIER : HungerLevel = 255;
                        Size--;
                        ScreenManager.food.Remove(pill);
                    }
                    //WanderingPoint = new Vector2(Misc.generaterValue(1, 50, ScreenManager.GraphicsDeviceMgr.PreferredBackBufferWidth - Texture.Width), Misc.generaterValue(1, (int)ScreenManager.water.GetHeight(Position.X), ScreenManager.GraphicsDeviceMgr.PreferredBackBufferHeight - Texture.Height));
                    if (pill.Position.Y >= ScreenManager.water.GetHeight(pill.Position.X))
                        WanderingPoint = getClosestPill();
                    CurrentMovementSpeed = (FISH_MOVEMENT * 2) * (float)gameTime.ElapsedGameTime.TotalMilliseconds; //twice as fast when fish find food
                    break;
                }
            }
            Position = moveNpcs(Position, WanderingPoint);
            float distance = Vector2.Distance(Position, WanderingPoint);
            if (distance < 5)
                WanderingPoint = Vector2.Zero;
            pillList.Clear();
        }

        private Vector2 getClosestPill()
        {
            int distance = 0;
                int index = 0;
            try
            {
                foreach (Food pill in ScreenManager.food.ToList())
                {
                    distance = (int)Vector2.Distance(pill.Position, Position);
                    pillList.Add(distance);
                }
                if (pillList.Count > 0)
                    index = IndexOfMin(pillList);
                if (ScreenManager.food.Count > 0)
                {
                    //ScreenManager.food[index].ObjectColor = Color.Red;
                    return ScreenManager.food[index].Position;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fout bij get closestpill.");
                Console.WriteLine(ex.Message);
            }
            return Vector2.Zero;
        }

        public static int IndexOfMin(IList<int> self)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            if (self.Count == 0)
            {
                throw new ArgumentException("List is empty.", "self");
            }

            int min = self[0];
            int minIndex = 0;

            for (int i = 1; i < self.Count; ++i)
            {
                if (self[i] < min)
                {
                    min = self[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }

        public Vector2 moveNpcs(Vector2 spot1, Vector2 spot2)
        {
            if (spot1.X + CurrentMovementSpeed < spot2.X)
            {
                spot1.X += CurrentMovementSpeed;
            }
            else if (spot1.X - CurrentMovementSpeed > spot2.X)
            {
                spot1.X -= CurrentMovementSpeed;
            }
            if (spot1.Y + CurrentMovementSpeed < spot2.Y)
            {
                spot1.Y += CurrentMovementSpeed;
            }
            else if (spot1.Y - CurrentMovementSpeed > spot2.Y)
            {
                spot1.Y -= CurrentMovementSpeed;
            }
            return spot1;
        }
    }
}
