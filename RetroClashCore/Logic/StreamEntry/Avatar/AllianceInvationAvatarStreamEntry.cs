using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroClashCore.Helpers;

namespace RetroClashCore.Logic.StreamEntry.Avatar
{
    public class AllianceInvationAvatarStreamEntry : AvatarStreamEntry
    {
        public AllianceInvationAvatarStreamEntry()
        {
            StreamEntryType = 4;
        }

        [JsonProperty("alliance_id")]
        public long AllianceId { get; set; }

        [JsonProperty("alliance_name")]
        public string AllianceName { get; set; }

        [JsonProperty("alliance_badge")]
        public int AllianceBadge { get; set; }

        [JsonProperty("sender_home_id")]
        public long SenderHomeId { get; set; }

        public override async Task Encode(MemoryStream stream)
        {
            await base.Encode(stream);

            await stream.WriteLongAsync(AllianceId); // AllianceId
            await stream.WriteStringAsync(AllianceName); // AllianceName
            await stream.WriteIntAsync(AllianceBadge); // AllianceBadge

            if (SenderHomeId > 0)
            {
                stream.WriteByte(1);
                await stream.WriteLongAsync(SenderHomeId); // SenderHomeId
            }
            else
            {
                stream.WriteByte(0);
            }
        }
    }
}