using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Logic.Manager.Items.Replay;
using RetroClash.Logic.StreamEntry.Avatar;

namespace RetroClash.Protocol.Messages.Server
{
    public class AvatarStreamMessage : PiranhaMessage
    {
        public AvatarStreamMessage(Device device) : base(device)
        {
            Id = 24411;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(1); // StreamEntryCount

            var log = new BattleLog
            {
                Loot = new []{new []{ 3000001, 1000 }, new []{ 3000002, 1000 } },
                Units = new []{new []{ 4000001 , 10 } },
                Levels = new []{new []{ 4000001 , 2} },
                Spells = new int[0][],
                
                Stats = new BattleLogStats
                {
                    TownHallDestroyed = true,
                    DestructionPercentage = 100,
                    AllianceName = "Elolia",
                    AllianceUsed = false,
                    AttackerScore = 25,
                    BattleEnded = true,
                    BattleTime = 90,
                    DefenderScore = -25,
                    HomeId = new []{0, 2},
                    OriginalScore = 100
                }
            };

            await new BattleReportStreamEntry
            {
                StreamEntryType = 7,
                MajorVersion = 5,
                Build = 2,
                ContentVersion = 4,
                CreationDateTime = DateTime.Now,
                IsRevengeUsed = true,
                SenderAvatarId = 5,
                SenderName = "Test",
                SenderLevel = 100,
                SenderLeagueType = 1,
                ShardId = 0,
                ReplayId = 1,
                BattleLogJson = JsonConvert.SerializeObject(log),
                Id = 1

            }.Encode(Stream);
        }
    }
}