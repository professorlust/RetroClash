using Newtonsoft.Json;

namespace RetroClash.Logic.Manager.Items.Replay
{
    public class ReplayUnitItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("cnt", DefaultValueHandling = DefaultValueHandling.Include)]
        public int Cnt { get; set; }
    }
}