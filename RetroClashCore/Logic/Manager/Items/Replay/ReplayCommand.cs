using Newtonsoft.Json;

namespace RetroClashCore.Logic.Manager.Items.Replay
{
    public class ReplayCommand
    {
        [JsonProperty("ct")]
        public int CommandType { get; set; }

        [JsonProperty("c")]
        public ReplayCommandInfo ReplayCommandInfo { get; set; }
    }
}