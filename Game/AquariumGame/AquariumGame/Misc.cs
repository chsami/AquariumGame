using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AquariumGame._0
{
    class Misc
    {

        static Random random = new Random();

        public static List<int> GenerateRandom(int count, int min, int max)
        {
            // generate count random values.
            HashSet<int> candidates = new HashSet<int>();
            while (candidates.Count < count)
            {
                // May strike a duplicate.
                candidates.Add(random.Next(min, max));
            }

            // load them in to a list.
            List<int> result = new List<int>();
            result.AddRange(candidates);

            // shuffle the results:
            int i = result.Count;
            while (i > 1)
            {
                i--;
                int k = random.Next(i + 1);
                int value = result[k];
                result[k] = result[i];
                result[i] = value;
            }
            return result;
        }

        public static Boolean isEqualToNumber(int number, int probability, int min, int max)
        {
            List<int> vals = GenerateRandom(probability, min, max);
            foreach (var item in vals)
            {
                if (item == number)
                {
                    return true;
                }
            }
            return false;
        }

        public static int generaterValue(int probability, int min, int max)
        {
            List<int> vals = GenerateRandom(probability, min, max);
            return vals[random.Next(vals.Count)];
        }

        public static float incGameTime(GameTime gameTime, float current, float max)
        {
            if (current > max)
            {
                return 1.5f;
            }
            return 0f;
        }

        public static float scaleInterfacePosition(float num, string type)
        {
            if (type == "w")
            {
                return Screens.ScreenManager.GAMESCREEN_WIDTH * num / Screens.ScreenManager.DEFAULT_SCREEN_WIDTH;
            }
            else
            {
                return Screens.ScreenManager.GAMESCREEN_HEIGHT * num / Screens.ScreenManager.DEFAULT_SCREEN_HEIGHT;
            }
        }

    }
}
