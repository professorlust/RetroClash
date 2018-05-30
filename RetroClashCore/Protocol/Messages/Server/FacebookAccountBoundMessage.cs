using System.Threading.Tasks;
using RetroClashCore.Extensions;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class FacebookAccountBoundMessage : PiranhaMessage
    {
        public FacebookAccountBoundMessage(Device device) : base(device)
        {
            Id = 24201;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(1); 
        }
    }
}