using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroClashCore.Helpers;

namespace RetroClashCore.Logic.Slots
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
            var player = await Resources.PlayerCache.GetPlayer(AccountId.Long);

            await AccountId.Encode(stream);  // Avatar Id
            await stream.WriteStringAsync(null); // FacebookId
            await stream.WriteStringAsync(player.Name); // Name
            await stream.WriteIntAsync(Role); // Role
            await stream.WriteIntAsync(player.ExpLevel); // Exp Level
            await stream.WriteIntAsync(LogicUtils.GetLeagueByScore(Score)); // League Type
            await stream.WriteIntAsync(Score); // Score
            await stream.WriteIntAsync(Donations); // Donations
            await stream.WriteIntAsync(DonationsReceived); // Donations Received
            await stream.WriteIntAsync(order); // Order
            await stream.WriteIntAsync(order); // Previous Order

            stream.WriteByte(0); // IsNewMember

            stream.WriteByte(1); // HasHomeId
            await AccountId.Encode(stream);  // Home Id
        }
    }
}