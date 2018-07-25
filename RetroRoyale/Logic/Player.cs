using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroGames.Helpers;
using RetroRoyale.Database;
using RetroRoyale.Logic.Manager;
using RetroRoyale.Logic.StreamEntry;

namespace RetroRoyale.Logic
{
    public class Player : IDisposable
    {
        [JsonProperty("achievements")] public Achievements Achievements = new Achievements();

        [JsonProperty("stream")] public List<AvatarStreamEntry> Stream = new List<AvatarStreamEntry>(20);

        public Player(long id, string token)
        {
            AccountId = id;
            Name = "RetroRoyale";
            PassToken = token;
            ExpLevel = 1;
            TutorialSteps = 10;
            Language = "EN";
            Diamonds = 1000;
            Gold = 500;
        }

        [JsonIgnore]
        public long AccountId
        {
            get => (long)HighId << 32 | (uint)LowId;
            set
            {
                HighId = Convert.ToInt32(value >> 32);
                LowId = (int)value;
            }
        }

        [JsonProperty("hi_id")]
        public int HighId { get; set; }

        [JsonProperty("lo_id")]
        public int LowId { get; set; }

        [JsonProperty("alliance_id")]
        public long AllianceId { get; set; }

        [JsonProperty("fb_id")]
        public string FacebookId { get; set; }

        [JsonProperty("account_name")]
        public string Name { get; set; }

        [JsonProperty("pass_token")]
        public string PassToken { get; set; }

        [JsonProperty("exp_level")]
        public int ExpLevel { get; set; }

        [JsonProperty("exp_points")]
        public int ExpPoints { get; set; }

        [JsonProperty("tutorial_steps")]
        public int TutorialSteps { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonIgnore]
        public Device Device { get; set; }

        [JsonProperty("diamonds")]
        public int Diamonds { get; set; }

        [JsonProperty("gold")]
        public int Gold { get; set; }

        public void Dispose()
        {
            Device = null;
            Stream = null;
            Achievements = null;
        }

        public void AddEntry(AvatarStreamEntry entry)
        {
        }

        public async Task LogicClientHome(MemoryStream stream)
        {
            await stream.WriteLong(AccountId); 

            await stream.WriteVInt(1);
            await stream.WriteVInt(1);
            await stream.WriteVInt(239940);
            await stream.WriteVInt(346065);
            await stream.WriteVInt(Utils.GetCurrentTimestamp);
            await stream.WriteVInt(0);

            await stream.WriteVInt(3); // DECK COUNT

            await stream.WriteVInt(8); // CARDS IN DECK 1
            await stream.WriteVInt(26000001);
            await stream.WriteVInt(26000002);
            await stream.WriteVInt(26000003);
            await stream.WriteVInt(28000004);
            await stream.WriteVInt(28000005);
            await stream.WriteVInt(26000006);
            await stream.WriteVInt(26000007);
            await stream.WriteVInt(26000008);

            await stream.WriteVInt(8); // CARDS IN DECK 2
            await stream.WriteVInt(26000001);
            await stream.WriteVInt(26000002);
            await stream.WriteVInt(26000003);
            await stream.WriteVInt(26000004);
            await stream.WriteVInt(26000005);
            await stream.WriteVInt(26000006);
            await stream.WriteVInt(26000007);
            await stream.WriteVInt(26000008);

            await stream.WriteVInt(8); // CARDS IN DECK 3
            await stream.WriteVInt(26000001);
            await stream.WriteVInt(26000002);
            await stream.WriteVInt(26000003);
            await stream.WriteVInt(26000004);
            await stream.WriteVInt(26000005);
            await stream.WriteVInt(26000006);
            await stream.WriteVInt(26000007);
            await stream.WriteVInt(26000008);

            stream.WriteByte(0xFF);

            await stream.WriteVInt(26);
            await stream.WriteVInt(1);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);

            await stream.WriteVInt(26);
            await stream.WriteVInt(2);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);

            await stream.WriteVInt(26);
            await stream.WriteVInt(3);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);

            await stream.WriteVInt(26);
            await stream.WriteVInt(4);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);

            await stream.WriteVInt(26);
            await stream.WriteVInt(5);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);

            await stream.WriteVInt(26);
            await stream.WriteVInt(6);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);

            await stream.WriteVInt(26);
            await stream.WriteVInt(7);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);

            await stream.WriteVInt(26);
            await stream.WriteVInt(8);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(1);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);

            await stream.WriteVInt(1); // COUNT

            await stream.WriteVInt(26);
            await stream.WriteVInt(0);
            await stream.WriteVInt(12);
            await stream.WriteVInt(0);
            await stream.WriteVInt(1);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0); // Is new = 2, 0 = Old

            await stream.WriteVInt(0);

            // CHESTS?
            await stream.WriteVInt(0);
            await stream.WriteVInt(4); // SLOT COUNT

            await stream.WriteVInt(7);
            await stream.WriteVInt(19);
            await stream.WriteVInt(6);
            await stream.WriteVInt(0); // CHEST READY TO OPEN
            await stream.WriteVInt(1);
            await stream.WriteVInt(6);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(287320);
            await stream.WriteVInt(288000);
            await stream.WriteVInt(1532522384); // Timestamp

            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(127);

            await stream.WriteVInt(0);
            await stream.WriteVInt(0);

            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(127);

            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(127);

            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);

            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);

            await stream.WriteVInt(2);

            await stream.WriteVInt(12); // Avatar Level

            await stream.WriteVInt(54); // Arena HighId
            await stream.WriteVInt(8); // Arena LowId

            await stream.WriteVInt(1675707269);

            await stream.WriteVInt(3);
            await stream.WriteVInt(3);

            await stream.WriteVInt(300000); // Shop Refresh Time
            await stream.WriteVInt(300000); // Shop Refresh Time

            await stream.WriteVInt(686977031);

            await stream.WriteVInt(3); // Shop Cards Count

            await stream.WriteVInt(27);
            await stream.WriteVInt(2);
            await stream.WriteVInt(0);

            await stream.WriteVInt(28);
            await stream.WriteVInt(1);
            await stream.WriteVInt(0);

            await stream.WriteVInt(26);
            await stream.WriteVInt(5);
            await stream.WriteVInt(0);

            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(127);

            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(127);

            await stream.WriteVInt(2);
            await stream.WriteVInt(0);

            await stream.WriteVInt(0);
            await stream.WriteVInt(2);

            await stream.WriteVInt(1507);
            await stream.WriteVInt(12);

            await stream.WriteVInt(1);
            await stream.WriteVInt(170);

            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
            await stream.WriteVInt(0);
        }

        public async Task LogicClientAvatar(MemoryStream stream)
        {
            await stream.WriteVInt(HighId); // HighId
            await stream.WriteVInt(LowId); // LowId 
            await stream.WriteVInt(HighId); // HighId
            await stream.WriteVInt(LowId); // LowId 
            await stream.WriteVInt(HighId); // HighId
            await stream.WriteVInt(LowId); // LowId

            await stream.WriteString(Name); // Name
            await stream.WriteVInt(1); // NameSetByUser
            await stream.WriteVInt(0); // NameChangeState

            await stream.WriteVInt(54); // Arena HighId
            await stream.WriteVInt(7); // Arena LowId

            await stream.WriteVInt(3000); // PrestigeScore

            await stream.WriteVInt(0); // BestAvatarTournamentId
            await stream.WriteVInt(2); // BestAvatarTournamentRank
            await stream.WriteVInt(0); // BestAvatarTournamentScore
            await stream.WriteVInt(0); // LastAvatarTournamentId
            await stream.WriteVInt(0); // LastAvatarTournamentRank
            await stream.WriteVInt(0); // LastAvatarTournamentScore
            await stream.WriteVInt(0); // LastAvatarTournamentPrestigeGained

            await stream.WriteVInt(0);
            await stream.WriteVInt(4);
            await stream.WriteVInt(6);

            await stream.WriteVInt(3); // Count

            await stream.WriteVInt(5);
            await stream.WriteVInt(1);
            await stream.WriteVInt(500000);

            // CROWN CHEST
            await stream.WriteVInt(5);
            await stream.WriteVInt(4);
            await stream.WriteVInt(0);

            // GOLD
            await stream.WriteVInt(5);
            await stream.WriteVInt(5);
            await stream.WriteVInt(500000);

            await stream.WriteVInt(0);

            await stream.WriteVInt(0); // ACHIEVEMENTS

            await stream.WriteVInt(0); // COUNT

            await stream.WriteVInt(1);
            await stream.WriteVInt(5);
            await stream.WriteVInt(8);
            await stream.WriteVInt(9);

            await stream.WriteVInt(0); // COUNT

            await stream.WriteVInt(1000); // Diamonds
            await stream.WriteVInt(1000); // FreeDiamonds

            await stream.WriteVInt(0); // ExpPoints
            await stream.WriteVInt(12); // ExpLevel

            await stream.WriteVInt(12); // AvatarUserLevelTier

            await stream.WriteVInt(0); // HasAlliance
        }

        public async Task AvatarRankingEntry(MemoryStream stream)
        {
        }

        public void AddDiamonds(int value)
        {
            Diamonds += value;
        }

        public bool UseDiamonds(int value)
        {
            if (Diamonds < value)
                return false;

            Diamonds -= value;

            return true;
        }

        public async Task Update()
        {
            await Redis.CachePlayer(this);
        }
    }
}