using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RetroClash.Logic.Manager.Items
{
    public class BattleLog
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        public async Task Encode(MemoryStream stream)
        {
        }
    }
}