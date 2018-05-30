using System.Threading.Tasks;
using RetroClashCore.Extensions;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class AllianceJoinFailedMessage : PiranhaMessage
    {
        public AllianceJoinFailedMessage(Device device) : base(device)
        {
            Id = 24302;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(1);
        }
    }
}