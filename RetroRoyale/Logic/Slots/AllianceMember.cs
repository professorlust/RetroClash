using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroGames.Logic;

namespace RetroRoyale.Logic.Slots
{
    public class AllianceMember
    {
        public AllianceMember(LogicLong id, Enums.Role role, int score)
        {
            AccountId = id;
            Role = (int) role;
            Score = score;
        }

        [JsonProperty("account_id")]
        public LogicLong AccountId { get; set; }

        [JsonProperty("role")]
        public int Role { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("donations")]
        public int Donations { get; set; }

        [JsonProperty("donations_received")]
        public int DonationsReceived { get; set; }

        [JsonIgnore]
        public bool IsOnline => Resources.PlayerCache.ContainsKey(AccountId.Long);

        public async Task AllianceMemberEntry(MemoryStream stream, int order)
        {
        }
    }
}