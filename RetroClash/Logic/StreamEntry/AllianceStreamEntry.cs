using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroClash.Extensions;
using RetroClash.Logic.StreamEntry.Alliance;

namespace RetroClash.Logic.StreamEntry
{
    public class AllianceStreamEntry
    {
        [JsonProperty("type")]
        public int StreamEntryType { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("sender_id")]
        public long SenderAvatarId { get; set; }

        [JsonProperty("sender_name")]
        public string SenderName { get; set; }

        [JsonProperty("sender_role")]
        public int SenderRole { get; set; }

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
            await stream.WriteLongAsync(Id); // StreamEntryId
            await stream.WriteLongAsync(SenderAvatarId); // SenderAvatarId
            await stream.WriteLongAsync(SenderAvatarId); // HomeId
            await stream.WriteStringAsync(SenderName); // SenderName
            await stream.WriteIntAsync(SenderLevel); // SenderLevel
            await stream.WriteIntAsync(SenderLeagueType); // SenderLeagueType
            await stream.WriteIntAsync(SenderRole); // SenderRole
            await stream.WriteIntAsync(AgeSeconds); // AgeSeconds
        }

        public AllianceStreamEntry CreatetreamEntryByType(int type)
        {
            AllianceStreamEntry entry;

            switch (type)
            {
                case 2:
                {
                    entry = new ChatStreamEntry();
                    break;
                }

                case 3:
                {
                    entry = new JoinRequestAllianceStreamEntry();
                    break;
                }

                case 5:
                {
                    entry = new ReplayStreamEntry();
                    break;
                }

                default:
                {
                    return null;
                }
            }

            return entry;
        }
    }
}