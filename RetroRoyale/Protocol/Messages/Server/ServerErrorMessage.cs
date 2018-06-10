using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class ServerErrorMessage : PiranhaMessage
    {
        public ServerErrorMessage(Device device) : base(device)
        {
            Id = 24115;
        }

        public string Reason { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteString(Reason);
        }
    }
}