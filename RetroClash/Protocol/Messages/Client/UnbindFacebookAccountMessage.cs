using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class UnbindFacebookAccountMessage : Message
    {
        public UnbindFacebookAccountMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            Device.Player.FacebookId = null;

            await Resources.Gateway.Send(new FacebookAccountUnboundMessage(Device));
        }
    }
}