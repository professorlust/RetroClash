using Newtonsoft.Json;
using RetroClash.Logic.Manager.Items;

namespace RetroClash.Logic.Manager
{
    public class LogicReplay
    {
        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }

        [JsonProperty("end_tick")]
        public int EndTick { get; set; }

        [JsonProperty("attacker")]
        public ReplayProfile Attacker { get; set; }

        [JsonProperty("defender")]
        public ReplayProfile Defdender { get; set; }
    }
}