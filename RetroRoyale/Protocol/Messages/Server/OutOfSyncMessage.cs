using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class OutOfSyncMessage : PiranhaMessage
    {
        public OutOfSyncMessage(Device device) : base(device)
        {
            Id = 24104;
        }

        public override async Task Encode()
        {
            await Stream.WriteInt(0);
            await Stream.WriteInt(0);
            await Stream.WriteInt(0);
        }
    }
}