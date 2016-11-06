using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquariumGame._0.User
{
    class Player
    {
        static int gold;
        static int score;
        static int moneyBuffer;
        static string name;

        public static int MoneyBuffer
        {
            get { return moneyBuffer; }
            set { moneyBuffer = value; }
        }
        

        public static int Gold
        {
            get
            {
                return gold;
            }
            set
            {
                gold = value;
            }
        }

        public static int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }

        public static string Name
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
    }
}
