using System;
using System.Threading.Tasks;
using RetroClash.Database;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash
{
    public class Program
    {
        private Resources _resources;

        private static void Main(string[] args)
        {
            new Program().StartAsync().GetAwaiter().GetResult();
        }

        public async Task StartAsync()
        {
            Console.Title = "RetroClash Server v0.6";

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(
                "\r\n________     _____             ______________             ______  \r\n___  __ \\______  /_______________  ____/__  /_____ __________  /_ \r\n__  /_/ /  _ \\  __/_  ___/  __ \\  /    __  /_  __ `/_  ___/_  __ \\\r\n_  _, _//  __/ /_ _  /   / /_/ / /___  _  / / /_/ /_(__  )_  / / /\r\n/_/ |_| \\___/\\__/ /_/    \\____/\\____/  /_/  \\__,_/ /____/ /_/ /_/ \r\n                                                                  \r\n");

            Console.SetOut(new Prefixed());

            Console.WriteLine("Starting...");
            Console.ResetColor();

            _resources = new Resources();

            Console.ResetColor();

            while (true)
            {
                var key = Console.ReadKey(true).Key;

                Console.ForegroundColor = ConsoleColor.DarkYellow;

                switch (key)
                {
                    case ConsoleKey.D:
                    {
                        Configuration.Debug = !Configuration.Debug;
                        Console.WriteLine("Debugging has been " + (Configuration.Debug ? "enabled." : "disabled."));
                        break;
                    }

                    case ConsoleKey.H:
                    {
                        Console.WriteLine("Commands: [D]ebug, [H]elp, [K]ey, [M]aintenance, [S]tatus");
                        break;
                    }

                    case ConsoleKey.K:
                    {
                        Console.WriteLine($"Generated RC4 Key: {Utils.GenerateRc4Key}");
                        break;
                    }

                    case ConsoleKey.M:
                    {
                        Configuration.Maintenance = !Configuration.Maintenance;

                        if (Configuration.Maintenance)
                            try
                            {
                                Console.WriteLine("Removing every Player in cache...");
                                foreach (var player in Resources.PlayerCache.Values)
                                    player.Device.Disconnect();
                                Console.WriteLine("Done!");
                            }
                            catch (Exception exception)
                            {
                                Logger.Log(exception, Enums.LogType.Error);
                            }

                        Console.WriteLine("Maintenance has been " +
                                          (Configuration.Maintenance ? "enabled." : "disabled."));
                        break;
                    }

                    case ConsoleKey.S:
                    {
                        Console.WriteLine(
                            $"[STATUS] Online Players: {Resources.PlayerCache.Count}, Players Saved: {await MySQL.PlayerCount()}");
                        break;
                    }

                    default:
                    {
                        Console.WriteLine("Invalid Key. Press 'H' for help.");
                        break;
                    }
                }

                Console.ResetColor();
            }
        }
    }
}