using System.Collections.Generic;
using System.Timers;
using RetroClash.Files;
using RetroClash.Files.Logic;
using RetroClash.Logic;

namespace RetroClash.Database.Caching
{
    public class LeaderboardCache
    {
        private readonly Timer _timer = new Timer(20000)
        {
            AutoReset = true
        };

        public Alliance[] GlobalAlliances = new Alliance[200];
        public Player[] GlobalPlayers = new Player[200];

        public Alliance[] JoinableClans = new Alliance[40];

        public Dictionary<string, Player[]> LocalPlayers = new Dictionary<string, Player[]>(11);

        public LeaderboardCache()
        {
            _timer.Elapsed += TimerCallback;
            _timer.Start();

            foreach (var locales in Csv.Tables.Get(Enums.Gamefile.Locales).GetDatas())
                LocalPlayers.Add(((Locales) locales).Name, new Player[200]);
        }

        public async void TimerCallback(object state, ElapsedEventArgs args)
        {
            var currentGlobalAllianceRanking = await MySQL.GetGlobalAllianceRanking();
            for (var i = 0; i < currentGlobalAllianceRanking.Count; i++)
                GlobalAlliances[i] = currentGlobalAllianceRanking[i];

            var currentGlobalPlayerRanking = await MySQL.GetGlobalPlayerRanking();
            for (var i = 0; i < currentGlobalPlayerRanking.Count; i++)
                GlobalPlayers[i] = currentGlobalPlayerRanking[i];

            foreach (var players in LocalPlayers)
            {
                var currentLocalPlayerRanking = await MySQL.GetLocalPlayerRanking(players.Key);
                for (var i = 0; i < currentLocalPlayerRanking.Count; i++)
                    players.Value[i] = currentLocalPlayerRanking[i];
            }

            var currentJoinableClans = await MySQL.GetJoinableAlliances(40);
            for (var i = 0; i < currentJoinableClans.Count; i++)
                JoinableClans[i] = currentJoinableClans[i];

            Logger.Log("The leaderboards have been updated.");
        }
    }
}