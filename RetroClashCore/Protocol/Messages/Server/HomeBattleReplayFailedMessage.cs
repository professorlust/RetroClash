using System.Threading.Tasks;
using RetroClashCore.Extensions;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class HomeBattleReplayFailedMessage : PiranhaMessage
    {
        public HomeBattleReplayFailedMessage(Device device) : base(device)
        {
            Id = 24116;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(1);
        }
    }
}