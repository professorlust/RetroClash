using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class AllianceJoinFailedMessage : Message
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