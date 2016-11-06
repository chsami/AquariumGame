using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquariumGame._0.Timers
{
    class PlayTime
    {
        static DateTime startTime = DateTime.Now;
        public static TimeSpan time;

        public static void UpdatePlayTime()
        {
            time = DateTime.Now.Subtract(startTime);
        }
    }
}
