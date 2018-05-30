using System.Threading.Tasks;
using RetroClashCore.Extensions;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class ShutdownStartedMessage : PiranhaMessage
    {
        public ShutdownStartedMessage(Device device) : base(device)
        {
            Id = 20161;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(0); // SecondsUntilShutdown
        }
    }
}