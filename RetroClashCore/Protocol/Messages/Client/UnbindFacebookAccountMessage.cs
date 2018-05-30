using System.Threading.Tasks;
using RetroClashCore.Extensions;
using RetroClashCore.Logic;
using RetroClashCore.Protocol.Messages.Server;

namespace RetroClashCore.Protocol.Messages.Client
{
    public class UnbindFacebookAccountMessage : PiranhaMessage
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