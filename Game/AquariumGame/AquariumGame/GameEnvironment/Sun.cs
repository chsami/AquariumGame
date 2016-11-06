using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquariumGame._0.GameEnvironment
{
    public class Sun
    {
        public static float sunRise = 1.0f;

        public const float SUN_MODIFIER = 0.0005f;

        static bool sunGoingDown = false;

        public static void Update(GameTime gameTime)
        {
            if (sunRise > 10)
                sunGoingDown = true;
            if (sunRise <= 1.0f)
                sunGoingDown = false;
            if (sunGoingDown)
            {
                sunRise -= SUN_MODIFIER * gameTime.ElapsedGameTime.Milliseconds / 60;
            }
            else
            {
                 sunRise += SUN_MODIFIER * gameTime.ElapsedGameTime.Milliseconds / 60;
            }
        }
    }
}
