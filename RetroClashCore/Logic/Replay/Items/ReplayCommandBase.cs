using Newtonsoft.Json;

namespace RetroClashCore.Logic.Manager.Items.Replay
{
    public class ReplayCommandBase
    {
        [JsonProperty("t")]
        public int Tick { get; set; }
    }
}