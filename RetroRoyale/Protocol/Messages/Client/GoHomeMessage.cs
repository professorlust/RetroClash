using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;
using RetroRoyale.Protocol.Messages.Server;

namespace RetroRoyale.Protocol.Messages.Client
{
    public class GoHomeMessage : PiranhaMessage
    {
        public GoHomeMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            if (Device.State != Enums.State.Home)
                await Resources.Gateway.Send(new OwnHomeDataMessage(Device));
            else
                Device.Disconnect();
        }
    }
}