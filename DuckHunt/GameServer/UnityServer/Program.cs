using NiloxUniversalLib.Logging;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
{
    class Program
    {
        #region Console close 
        static ConsoleEventDelegate handler;   // Keeps it from getting garbage collected
                                               // Pinvoke
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);

        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
            {
                Log.Warning("Closing Console shuting server down");
                Thread.Sleep(100);
                Quit(true);
                Thread.Sleep(100);
            }
            return false;
        }
        #endregion

        private static bool isRunning = false;

        public static Thread mainthread;
        public static Thread inputthread;

        static void Main(string[] args)
        {
            #region Console close
            handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(handler, true);
            #endregion

            Console.Title = "Server";
            isRunning = true;

            mainthread = new Thread(new ThreadStart(MainThread));
            mainthread.Start();

            inputthread = new Thread(new ThreadStart(InputThread));
            inputthread.Start();

            Server.Start(50, 26950);
        }

        public async static void Quit(bool alreadyclosing = false)
        {
            Server.kickAll();
            await Server.removefromDatabase();
            Log.Info("------------------------------------------------------------------------------");
            if (alreadyclosing == false)
            {
                Environment.Exit(0);
            }
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
