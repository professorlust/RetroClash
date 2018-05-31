﻿using System;
using System.Collections.Generic;
using System.Timers;
using RetroClashCore.Files;
using RetroClashCore.Files.Logic;
using RetroClashCore.Helpers;
using RetroClashCore.Logic;

namespace RetroClashCore.Database.Caching
{
    public class LeaderboardCache
    {
        private readonly Timer _timer = new Timer(20000)
        {
            AutoReset = true
        };

        public List<Alliance> GlobalAlliances = new List<Alliance>(200);
        public List<Player> GlobalPlayers = new List<Player>(200);

        public List<Alliance> JoinableClans = new List<Alliance>(40);

        public Dictionary<string, List<Player>> LocalPlayers = new Dictionary<string, List<Player>>(11);

        public LeaderboardCache()
        {
            _timer.Elapsed += TimerCallback;
            _timer.Start();

            foreach (var locales in Csv.Tables.Get(Enums.Gamefile.Locales).GetDatas())
                LocalPlayers.Add(((Locales) locales).Name, new List<Player>(200));
        }

        public async void TimerCallback(object state, ElapsedEventArgs args)
        {
            try
            {
                var currentGlobalAllianceRanking = await MySQL.GetGlobalAllianceRanking();
                for (var i = 0; i < currentGlobalAllianceRanking.Count; i++)
                    GlobalAlliances.UpdateOrInsert(i, currentGlobalAllianceRanking[i]);

                var currentGlobalPlayerRanking = await MySQL.GetGlobalPlayerRanking();
                for (var i = 0; i < currentGlobalPlayerRanking.Count; i++)
                    GlobalPlayers.UpdateOrInsert(i, currentGlobalPlayerRanking[i]);

                foreach (var players in LocalPlayers)
                {
                    var currentLocalPlayerRanking = await MySQL.GetLocalPlayerRanking(players.Key);
                    for (var i = 0; i < currentLocalPlayerRanking.Count; i++)
                        players.Value.UpdateOrInsert(i, currentLocalPlayerRanking[i]);
                }

                var currentJoinableClans = await MySQL.GetJoinableAlliances(40);
                for (var i = 0; i < currentJoinableClans.Count; i++)
                    JoinableClans.UpdateOrInsert(i, JoinableClans[i]);

                Logger.Log("The leaderboards have been updated.");
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
        }
    }
}