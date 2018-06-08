using System.IO;
using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class JoinableAllianceListMessage : PiranhaMessage
    {
        public JoinableAllianceListMessage(Device device) : base(device)
        {
            Id = 24304;
        }

        public override async Task Encode()
        {
            var clans = Resources.LeaderboardCache.JoinableClans;

            var count = 0;
            using (var buffer = new MemoryStream())
            {
                foreach (var clan in clans)
                {
                    if (clan == null) continue;
                    await buffer.WriteLongAsync(clan.Id); // Id
                    await buffer.WriteStringAsync(clan.Name); // Name
                    await buffer.WriteIntAsync(clan.Badge); // Badge
                    await buffer.WriteIntAsync(clan.Type); // Type
                    await buffer.WriteIntAsync(clan.Members.Count); // Member Count
                    await buffer.WriteIntAsync(clan.Score); // Score
                    await buffer.WriteIntAsync(clan.RequiredScore); // Required Score

                    if (count++ >= 39)
                        break;
                }


                await Stream.WriteIntAsync(count);
                await Stream.WriteBufferAsync(buffer.ToArray());
            }
        }
    }
}