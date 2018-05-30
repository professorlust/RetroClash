using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroClashCore.Extensions;

namespace RetroClashCore.Logic.StreamEntry
{
    public class AvatarStreamEntry
    {
        [JsonProperty("type")]
        public int StreamEntryType { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("sender_id")]
        public long SenderAvatarId { get; set; }

        [JsonProperty("sender_name")]
        public string SenderName { get; set; }

        [JsonProperty("sender_lvl")]
        public int SenderLevel { get; set; }

        [JsonProperty("sender_league")]
        public int SenderLeagueType { get; set; }

        [JsonProperty("creation")]
        public DateTime CreationDateTime { get; set; }

        [JsonIgnore]
        public int AgeSeconds => (int) (DateTime.Now - CreationDateTime).TotalSeconds;

        public virtual async Task Encode(MemoryStream stream)
        {
            await stream.WriteIntAsync(StreamEntryType); // StreamEntryType
            await stream.WriteLongAsync(Id); // StreamEntryId
            await stream.WriteLongAsync(SenderAvatarId); // SenderAvatarId
            await stream.WriteStringAsync(SenderName); // SenderName
            await stream.WriteIntAsync(SenderLevel); // SenderLevel
            await stream.WriteIntAsync(SenderLeagueType); // SenderLeagueType
            await stream.WriteIntAsync(AgeSeconds); // AgeSeconds
            stream.WriteBool(false); // IsNew
        }

        public void SetSender(Player player)
        {
            SenderName = player.Name;
            SenderAvatarId = player.AccountId;
            SenderLevel = player.ExpLevel;
            SenderLeagueType = LogicUtils.GetLeagueByScore(player.Score);
        }
    }
}