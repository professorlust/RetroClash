using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroClash.Extensions;

namespace RetroClash.Logic.StreamEntry.Alliance
{
    public class AllianceEventStreamEntry : AllianceStreamEntry
    {
        public AllianceEventStreamEntry()
        {
            StreamEntryType = 4;
        }

        [JsonProperty("event_type")]
        public Enums.AllianceEvent EventType { get; set; }

        [JsonProperty("avatar_id")]
        public long AvatarId { get; set; }

        [JsonProperty("avatar_name")]
        public string AvatarName { get; set; }

        public override async Task Encode(MemoryStream stream)
        {
            await base.Encode(stream);

            await stream.WriteIntAsync((int) EventType); // EventType
            await stream.WriteLongAsync(AvatarId); // AvatarId
            await stream.WriteStringAsync(AvatarName); // AvatarName
        }
    }
}