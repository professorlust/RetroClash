using Newtonsoft.Json;

namespace RetroClash.Logic.Manager.Items.Replay
{
    public class ReplayCommand
    {
        [JsonProperty("ct")]
        public int CommandType { get; set; }

        [JsonProperty("c")]
        public ReplayCommandInfo ReplayCommandInfo { get; set; }
    }
}