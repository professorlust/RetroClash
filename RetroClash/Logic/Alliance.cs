using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroClash.Extensions;
using RetroClash.Logic.Slots;

namespace RetroClash.Logic
{
    public class Alliance
    {
        public Alliance(long id)
        {
            Id = id;
            Name = "RetroClash";
            Badge = 13000000;
            Type = 0;
            RequiredScore = 0;
        }

        [JsonIgnore]
        public long Id { get; set; }

        [JsonIgnore]
        public bool IsFull => Members.Count == 50;

        [JsonIgnore]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("badge")]
        public int Badge { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("required_score")]
        public int RequiredScore { get; set; }

        [JsonIgnore]
        public int Score => Members.Sum(m => m.Score) / 2;

        [JsonProperty("members")]
        public List<AllianceMember> Members = new List<AllianceMember>(50);

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
            for (var i = 0; i < Members.Count; i++)
                await Members[i].AllianceMemberEntry(stream, i + 1);
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
    }
}