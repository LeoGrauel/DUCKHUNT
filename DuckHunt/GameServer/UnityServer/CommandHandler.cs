using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public static class CommandHandler
    {
        public static void handle(string msg)
        {
            switch (msg)
            {
                case "stop":
                case "end":
                case "quit":
                    {
                        Program.Quit();
                        break;
                    }
                case "re":
                case "restart":
                    {
                        Program.Restart();
                        break;
                    }
                default:
                    break;
            }
        }

    }
}
