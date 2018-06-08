using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroGames.Helpers;

namespace RetroClashCore.Logic.StreamEntry.Alliance
{
    public class JoinRequestAllianceStreamEntry : AllianceStreamEntry
    {
        public JoinRequestAllianceStreamEntry()
        {
            StreamEntryType = 3;
        }

        [JsonProperty("msg")]
        public string Message { get; set; }

        [JsonProperty("responder_name")]
        public string ResponderName { get; set; }

        [JsonProperty("state")]
        public int State { get; set; }

        public override async Task Encode(MemoryStream stream)
        {
            await base.Encode(stream);

            await stream.WriteStringAsync(Message); // Message
            await stream.WriteStringAsync(ResponderName); // ResponderName
            await stream.WriteIntAsync(State); // State
        }
    }
}