using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class FacebookAccountBoundMessage : Message
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