using Newtonsoft.Json;

namespace RetroClashCore.Logic.Manager.Items
{
    public class UnitProd
    {
        [JsonProperty("unit_type")]
        public int UnitType { get; set; }
    }
}