using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using RetroGames.Helpers;
using RetroRoyale.Files;
using RetroRoyale.Files.Logic;
using RetroRoyale.Logic;

namespace RetroRoyale.Database.Caching
{
    public class Leaderboards
    {
        private readonly Timer _timer = new Timer(20000)
        {
            AutoReset = true
        };

        public List<Player> GlobalPlayers = new List<Player>(200);
        public Dictionary<string, List<Player>> LocalPlayers = new Dictionary<string, List<Player>>(11);

        public Leaderboards()
        {
            _timer.Elapsed += TimerCallback;
            _timer.Start();

            foreach (var locales in Csv.Tables.Get(Enums.Gamefile.Locales).GetDatas())
                LocalPlayers.Add(((Locales) locales).Name, new List<Player>(200));
        }

        public async void TimerCallback(object state, ElapsedEventArgs args)
        {
            await Task.Run(async () =>
            {
                try
                {
                    var currentGlobalPlayerRanking = await PlayerDb.GetGlobalPlayerRanking();
                    for (var i = 0; i < currentGlobalPlayerRanking.Count; i++)
                        GlobalPlayers.UpdateOrInsert(i, currentGlobalPlayerRanking[i]);

                    foreach (var players in LocalPlayers)
                    {
                        var currentLocalPlayerRanking = await PlayerDb.GetLocalPlayerRanking(players.Key);
                        for (var i = 0; i < currentLocalPlayerRanking.Count; i++)
                            players.Value.UpdateOrInsert(i, currentLocalPlayerRanking[i]);
                    }
                }
                catch (Exception exception)
                {
                    Logger.Log(exception, Enums.LogType.Error);
                }
            });
        }
    }
}