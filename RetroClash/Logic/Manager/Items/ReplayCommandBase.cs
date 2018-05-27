using Newtonsoft.Json;

namespace RetroClash.Logic.Manager.Items
{
    public class ReplayCommandBase
    {
        [JsonProperty("t")]
        public int Tick { get; set; }
    }
}