using NiloxUniversalLib.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
{
    class Program
    {
        private static bool isRunning = false;

        public static Thread mainthread;
        public static Thread inputthread;

        static void Main(string[] args)
        {
            Console.Title = "Server";
            isRunning = true;

            mainthread = new Thread(new ThreadStart(MainThread));
            mainthread.Start();

            inputthread = new Thread(new ThreadStart(InputThread));
            inputthread.Start();

            Server.Start(50, 26950);

        }

        public async static void Quit()
        {
            Server.kickAll();
            await Server.removefromDatabase();
            Log.Info("------------------------------------------------------------------------------");
            Environment.Exit(0);
        }

        public static void Restart()
        {
            var p = Process.GetCurrentProcess();
            string n = p.ProcessName;
            Process.Start(n);
            Quit();
        }

        private static void MainThread()
        {
            Log.Info($"Main Thread starded. Running at {Constants.TICKS_PER_SEC} ticks per second");
            DateTime nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (nextLoop < DateTime.Now)
                {
                    GameLogic.Update();

                    nextLoop = nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(nextLoop - DateTime.Now);
                    }
                }
            }

            Log.Info("Main Thread ended");
        }

        private static void InputThread()
        {
            Log.Info("InputThrad started");
            while (isRunning)
            {
                string input = Console.ReadLine();
                CommandHandler.handle(input);
            }
            Log.Info("InputThread ended");
        }

    }
}
