using System;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using RetroGames.Helpers;
using RetroRoyale.Logic.Manager;

namespace RetroRoyale.Logic
{
    public class Player : IDisposable
    {
        [JsonProperty("achievements")] public Achievements Achievements = new Achievements();

        [JsonIgnore] public Timer Timer = new Timer(5000)
        {
            AutoReset = true
        };

        public Player(long id, string token)
        {
            AccountId = id;

            Name = "RetroRoyale";
            PassToken = token;
            ExpLevel = 1;
            TutorialSteps = 10;
            Language = "EN";
            Diamonds = 1000000000;
        }

        [JsonProperty("account_id")]
        public long AccountId { get; set; }

        [JsonProperty("alliance_id")]
        public long AllianceId { get; set; }

        [JsonProperty("fb_id")]
        public string FacebookId { get; set; }

        [JsonProperty("account_name")]
        public string Name { get; set; }

        [JsonProperty("device_name")]
        public string DeviceName { get; set; }

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

        public void Dispose()
        {
            Timer.Stop();

            Timer = null;
            Device = null;
            Achievements = null;
        }

        public async Task LogicClientHome(MemoryStream stream)
        {
        }

        public async Task LogicClientAvatar(MemoryStream stream)
        {
            await stream.WriteVInt(0);
            await stream.WriteVInt(1);

            await stream.WriteVInt(0);
            await stream.WriteVInt(1);

            await stream.WriteVInt(0);
            await stream.WriteVInt(1);

            await stream.WriteString(Name);
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
    }
}