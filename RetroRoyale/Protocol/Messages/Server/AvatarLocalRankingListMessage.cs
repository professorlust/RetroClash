using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class AvatarRankingListMessage : PiranhaMessage
    {
        public AvatarRankingListMessage(Device device) : base(device)
        {
            Id = 24403;
        }

        public override async Task Encode()
        {
            await Stream.WriteVInt(0);
            await Stream.WriteInt(0);
            await Stream.WriteInt(0);
        }
    }
}