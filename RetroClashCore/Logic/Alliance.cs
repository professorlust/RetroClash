using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using RetroClashCore.Database;
using RetroClashCore.Logic.Slots;
using RetroClashCore.Logic.StreamEntry;
using RetroGames.Helpers;
using RetroGames.Logic;

namespace RetroClashCore.Logic
{
    public class Alliance
    {
        [JsonProperty("members")] public List<AllianceMember> Members = new List<AllianceMember>(50);

        [JsonProperty("stream")] public List<AllianceStreamEntry> Stream = new List<AllianceStreamEntry>(40);

        [JsonIgnore] public Timer Timer = new Timer(5000)
        {
            AutoReset = true
        };

        public Alliance(long id)
        {
            Id = id;
            Name = "RetroClash";
            Badge = 13000000;
        }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonIgnore]
        public bool IsFull => Members.Count == 50;

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("badge")]
        public int Badge { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("r_score")]
        public int RequiredScore { get; set; }

        [JsonProperty("score")]
        public int Score => Members.Sum(m => m.Score) / 2;

        public async Task AllianceRankingEntry(MemoryStream stream)
        {
            await stream.WriteIntAsync(Badge); // Badge
            await stream.WriteIntAsync(Members.Count); // Member Count
        }

        public async Task AllianceFullEntry(MemoryStream stream)
        {
            await AllianceHeaderEntry(stream);

            await stream.WriteStringAsync(Description); // Description

            await stream.WriteIntAsync(Members.Count); // Member Count

            for (var i = 1; i < Members.Count + 1; i++)
                await Members[i].AllianceMemberEntry(stream, i);
        }

        public async Task AllianceHeaderEntry(MemoryStream stream)
        {
            await stream.WriteLongAsync(Id); // Id
            await stream.WriteStringAsync(Name); // Name
            await stream.WriteIntAsync(Badge); // Badge
            await stream.WriteIntAsync(Type); // Type
            await stream.WriteIntAsync(Members.Count); // Member Count
            await stream.WriteIntAsync(Score); // Score
            await stream.WriteIntAsync(RequiredScore); // Required Score
        }

        public void AddEntry(AllianceStreamEntry entry)
        {
            while (Stream.Count >= 40)
                Stream.RemoveAt(0);

            Stream.Add(entry);
        }

        public int GetRole(LogicLong id)
        {
            var index = Members.FindIndex(x => x.AccountId == id);

            return index > -1 ? Members[index].Role : 1;
        }

        public bool IsMember(long id)
        {
            return Members.FindIndex(x => x.AccountId == id) != -1;
        }

        public async void SaveCallback(object state, ElapsedEventArgs args)
        {
            if (Redis.IsConnected)
                await Redis.CacheAlliance(this);

            await AllianceDb.Save(this);
        }
    }
}