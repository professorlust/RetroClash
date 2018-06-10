using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class DisconnectedMessage : PiranhaMessage
    {
        public DisconnectedMessage(Device device) : base(device)
        {
            Id = 25892;
        }

        // ErrorCodes
        // 1 = Another Device is connecting

        public override async Task Encode()
        {
            await Stream.WriteInt(1);
        }
    }
}