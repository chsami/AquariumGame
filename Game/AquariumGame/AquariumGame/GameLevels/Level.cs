using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AquariumGame._0.Config;
using AquariumGame._0.Entities;
using AquariumGame._0.Screens;

namespace AquariumGame._0.GameLevels
{
    class Level
    {
        public static bool nextLevel = false;
        public static int currentLevel;
        public static float percentageTillNextLevel;
        private static int timer;
        private const int DELAY = 200;

        public static void Update()
        {
            if (percentageTillNextLevel >= WorldConfigurator.NUMBER_TILL_NEXT_LEVEL && nextLevel == false)
            {
                nextLevel = true;
                timer = DELAY;
            }
            if (nextLevel && timer == DELAY)
            {
                foreach (var image in Screens.ScreenManager.images)
                {
                    if (image.Name.Contains("level_cleared"))
                        image.Visible = true;
                }
            }
            if (timer > 0)
            {
                timer--;
            }
            if (timer <= 0 && nextLevel)
            {
                foreach (var image in Screens.ScreenManager.images)
                {
                    if (image.Name.Contains("level_cleared"))
                        image.Visible = false;
                }
                nextLevel = false;
                percentageTillNextLevel = 0f;
                timer = DELAY;
                ScreenManager.ChangeScreens(ScreenManager.getCurrentScreen(), ScreenManager.playScreen);
                EnemyManager.enemySpawnTimer = Enemy.ENEMY_SPAWN;
            }
        }
    }
}
