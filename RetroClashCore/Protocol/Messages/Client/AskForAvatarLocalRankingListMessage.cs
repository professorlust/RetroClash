using System.Threading.Tasks;
using RetroClashCore.Extensions;
using RetroClashCore.Logic;
using RetroClashCore.Protocol.Messages.Server;

namespace RetroClashCore.Protocol.Messages.Client
{
    public class AskForAvatarLocalRankingListMessage : PiranhaMessage
    {
        public AskForAvatarLocalRankingListMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new AvatarLocalRankingListMessage(Device));
        }
    }
}