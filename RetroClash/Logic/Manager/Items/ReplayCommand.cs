using Newtonsoft.Json;

namespace RetroClash.Logic.Manager.Items
{
    public class ReplayCommand
    {
        [JsonProperty("ct")]
        public int CommandType { get; set; }

        [JsonProperty("c")]
        public ReplayCommandInfo ReplayCommandInfo { get; set; }
    }
}