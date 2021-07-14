using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public static class Game
    {
        public static int pointsblue = 0;
        public static int pointsred = 0;

        public static int bluecount = 0;
        public static int redcount = 0;


        public static int nextTeamid()
        {
            if (bluecount > redcount)
            {
                return 1;
            }
            if (redcount > bluecount)
            {
                return 0;
            }

            return 0;
        }
    }
}
