using System.Threading.Tasks;
using RetroClashCore.Helpers;
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