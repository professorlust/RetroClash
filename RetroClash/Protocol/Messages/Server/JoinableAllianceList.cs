using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class JoinableAllianceList : Message
    {
        public JoinableAllianceList(Device device) : base(device)
        {
            Id = 24304;
        }

        public override async Task Encode()
        {
            var clans = Resources.LeaderboardCache.JoinableClans;

            await Stream.WriteIntAsync(clans.Length);

            foreach(var clan in clans)
            {
                await Stream.WriteLongAsync(clan.Id); // Id
                await Stream.WriteStringAsync(clan.Name); // Name
                await Stream.WriteIntAsync(clan.Badge); // Badge
                await Stream.WriteIntAsync(clan.Type); // Type
                await Stream.WriteIntAsync(clan.Members.Count); // Member Count
                await Stream.WriteIntAsync(clan.Score); // Score
                await Stream.WriteIntAsync(clan.RequiredScore); // Required Score
            }
        }
    }
}