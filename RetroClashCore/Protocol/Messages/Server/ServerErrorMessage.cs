using System.Threading.Tasks;
using RetroClashCore.Extensions;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class ServerErrorMessage : PiranhaMessage
    {
        public ServerErrorMessage(Device device) : base(device)
        {
            Id = 24115;
        }

        public string Reason { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteStringAsync(Reason);
        }
    }
}