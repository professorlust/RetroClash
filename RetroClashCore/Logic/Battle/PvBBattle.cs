using System;
using Newtonsoft.Json;
using RetroClashCore.Helpers;
using RetroClashCore.Logic.Replay;
using RetroClashCore.Logic.Replay.Items;
using RetroClashCore.Logic.StreamEntry.Avatar;

namespace RetroClashCore.Logic.Battle
{
    public class PvbBattle
    {
        public LogicReplay Replay = new LogicReplay();

        public Player Attacker { get; set; }

        public Player Defender { get; set; }

        public PvbBattle(Player attacker)
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
            var random = new Random();

            var attackerReward = random.Next(10, 25);

            Attacker.Score += attackerReward;

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
                    Loot = new[] { new[] { 3000001, random.Next(1000, 100000) }, new[] { 3000002, random.Next(1000, 100000) } },
                    Units = new[] { new[] { 4000000, random.Next(10, 50) }, new[] { 4000001, random.Next(10, 50) }, new[] { 4000002, random.Next(10, 50) }, new[] { 4000003, random.Next(10, 50) }, new[] { 4000004, random.Next(10, 50) }, new[] { 4000005, random.Next(10, 50) } },
                    Levels = new int[0][],
                    Spells = new int[0][],

                    Stats = new BattleLogStats
                    {
                        TownHallDestroyed = true,
                        DestructionPercentage = random.Next(0, 100),
                        AllianceName = "RetroClash",
                        AllianceUsed = false,
                        AttackerScore = attackerReward,
                        BattleEnded = true,
                        BattleTime = 1,
                        DefenderScore = random.Next(-30, -15),
                        HomeId = new []{0, 1},
                        OriginalScore = Attacker.Score
                    }
                })
            };
        }
    }
}