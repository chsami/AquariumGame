using AquariumGame._0.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquariumGame._0.Entities
{
    public static class EnemyManager
    {

        public static float enemySpawnTimer = Enemy.ENEMY_SPAWN;

        public static void Update(GameTime gameTime)
        {
            if (enemySpawnTimer > 0)
            {
                enemySpawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (enemySpawnTimer <= 0 && ScreenManager.enemies.Count == 0)
            {
                ScreenManager.addEnemy("octochaos", "octopus", new Vector2(250, 250), 255, 2);
                enemySpawnTimer = Enemy.ENEMY_SPAWN * 5;
            }
        }
        
    }
}
