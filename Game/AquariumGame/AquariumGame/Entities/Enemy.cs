using AquariumGame._0.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquariumGame._0.Entities
{
    public class Enemy : Entity
    {

        private static bool receiveDamage = false;

        public static bool ReceiveDamage
        {
            get { return receiveDamage; }
            set { receiveDamage = value; }
        }
        
        private int damage;
        private const float MOVEMENT_SPEED = 0.05f;
        public static float ENEMY_SPAWN = 1000f;

        public Enemy(string name, Texture2D texture, Vector2 position, int health, int damage)
        {
            Name = name;
            Texture = texture;
            Position = position;
            Health = health;
            EntColor = Color.White;
            this.damage = damage;
        }


        public Vector2 MoveToFish()
        {
            Vector2 moveTo = new Vector2(0, 0);
            Vector2 currentEnemyPosition = Position;
            int distance = 0;
            const int OFFSET = 122;
            foreach (var fish in Screens.ScreenManager.friendly)
            {
                if (fish != null)
                {
                    moveTo = fish.Position;
                    if (currentEnemyPosition.X + CurrentMovementSpeed < moveTo.X - (Texture.Width / 2))
                    {
                        currentEnemyPosition.X += CurrentMovementSpeed;
                    }
                    else if (currentEnemyPosition.X - CurrentMovementSpeed > moveTo.X - (Texture.Width / 2))
                    {
                        currentEnemyPosition.X -= CurrentMovementSpeed;
                    }
                    if (currentEnemyPosition.Y + CurrentMovementSpeed < moveTo.Y - (Texture.Height / 2))
                    {
                        currentEnemyPosition.Y += CurrentMovementSpeed;
                    }
                    else if (currentEnemyPosition.Y - CurrentMovementSpeed > moveTo.Y - (Texture.Height / 2))
                    {
                        currentEnemyPosition.Y -= CurrentMovementSpeed;
                    }
                    distance = (int)Vector2.Distance(currentEnemyPosition, moveTo);
                    if (distance <= OFFSET)
                    {
                        foreach (var font in Screens.ScreenManager.fonts)
                        {
                            if (!font.Visible && font.Name.Equals("enemyFont"))
                            {
                                font.Visible = true;
                                font.Position = currentEnemyPosition;
                            }
                            else if (font.Visible && font.Name.Equals("enemyFont"))
                            {
                                font.Position = currentEnemyPosition;
                            }
                        }
                        fish.Health -= damage;
                    }
                    return currentEnemyPosition;
                }
            }
            return Vector2.Zero;
        }

        

        public override void UpdateEntity(GameTime gameTime)
        {
            if (receiveDamage)
            {
                Health -= 10;
                EntColor = new Color(EntColor.R, Health, Health);
                receiveDamage = false;
            }
            CurrentMovementSpeed = MOVEMENT_SPEED * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            Position = MoveToFish();
            if (Health <= 0 || Position == Vector2.Zero)
            {
                ScreenManager.enemies.Remove(this);
            }
        }
    }
}
