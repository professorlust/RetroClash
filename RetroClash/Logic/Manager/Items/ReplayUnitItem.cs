using Newtonsoft.Json;

namespace RetroClash.Logic.Manager.Items
{
    public class ReplayUnitItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("cnt")]
        public int Cnt { get; set; }
    }
}