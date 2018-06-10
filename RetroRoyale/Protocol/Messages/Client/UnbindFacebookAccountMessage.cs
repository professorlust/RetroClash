using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;
using RetroRoyale.Protocol.Messages.Server;

namespace RetroRoyale.Protocol.Messages.Client
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