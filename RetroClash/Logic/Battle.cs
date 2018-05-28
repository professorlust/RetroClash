using System;
using Newtonsoft.Json;
using RetroClash.Extensions;
using RetroClash.Logic.Manager;
using RetroClash.Logic.Manager.Items.Replay;
using RetroClash.Logic.StreamEntry.Avatar;

namespace RetroClash.Logic
{
    public class Battle
    {
        public LogicReplay Replay = new LogicReplay();

        public Player Attacker { get; set; }

        public Player Defender { get; set; }

        public Battle(Player attacker)
        {
            Attacker = attacker;
            Replay.Attacker = Attacker.GetReplayProfile(true);
            Replay.Level = Attacker.LogicGameObjectManager;
        }

        public void SetDefender(Player defender)
        {
            Replay.Timestamp = Utils.GetCurrentTimestamp;
            Defender = defender;
            Replay.Defender = Defender.GetReplayProfile(false);
        }

        public void RecordCommand(ReplayCommand cmd)
        {
            if (!Replay.Commands.Contains(cmd) && Replay.Commands.Count < 500)
                Replay.Commands.Add(cmd);
        }

        public string GetReplayJson => JsonConvert.SerializeObject(Replay, new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None
        });

        public BattleReportStreamEntry GetBattleReportStreamEntry(long replayId)
        {
            return new BattleReportStreamEntry
            {
                MajorVersion = 5,
                Build = 2,
                ContentVersion = 4,
                CreationDateTime = DateTime.Now,
                IsRevengeUsed = true, // Revenge is not supported atm
                SenderAvatarId = Defender.AccountId,
                SenderName = Defender.Name,
                SenderLevel = Defender.ExpLevel,
                SenderLeagueType = LogicUtils.GetLeagueByScore(Defender.Score),
                ShardId = 0,
                ReplayId = replayId,
                BattleLogJson = JsonConvert.SerializeObject(new BattleLog
                {
                    // Here we use random values
                    Loot = new[] { new[] { 3000001, 1000 }, new[] { 3000002, 1000 } },
                    Units = new[] { new[] { 4000001, 80 } },
                    Levels = new[] { new[] { 4000001, 3 } },
                    Spells = new int[0][],

                    Stats = new BattleLogStats
                    {
                        TownHallDestroyed = true,
                        DestructionPercentage = 100,
                        AllianceName = "RetroClash",
                        AllianceUsed = false,
                        AttackerScore = 25,
                        BattleEnded = true,
                        BattleTime = 90,
                        DefenderScore = -25,
                        HomeId = new[] { 0, 2 },
                        OriginalScore = 100
                    }
                })
            };
        }
    }
}