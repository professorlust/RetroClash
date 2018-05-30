using System.Threading.Tasks;
using RetroClashCore.Helpers;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class AllianceChangeFailedMessage : PiranhaMessage
    {
        public AllianceChangeFailedMessage(Device device) : base(device)
        {
            Id = 24333;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(0);
        }
    }
}