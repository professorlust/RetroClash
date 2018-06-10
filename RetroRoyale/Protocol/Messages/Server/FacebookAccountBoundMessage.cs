using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class FacebookAccountBoundMessage : PiranhaMessage
    {
        public FacebookAccountBoundMessage(Device device) : base(device)
        {
            Id = 24201;
        }

        public override async Task Encode()
        {
            await Stream.WriteInt(1);
        }
    }
}