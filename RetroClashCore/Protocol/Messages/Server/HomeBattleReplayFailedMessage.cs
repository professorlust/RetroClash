using System.Threading.Tasks;
using RetroClashCore.Helpers;
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