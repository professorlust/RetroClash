using System.Threading.Tasks;
using RetroClashCore.Extensions;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class OutOfSyncMessage : PiranhaMessage
    {
        public OutOfSyncMessage(Device device) : base(device)
        {
            Id = 24104;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(0);
            await Stream.WriteIntAsync(0);
            await Stream.WriteIntAsync(0);
        }
    }
}