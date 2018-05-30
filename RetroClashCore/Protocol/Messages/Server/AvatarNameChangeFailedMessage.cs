using System.Threading.Tasks;
using RetroClashCore.Extensions;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class AvatarNameChangeFailedMessage : PiranhaMessage
    {
        public AvatarNameChangeFailedMessage(Device device) : base(device)
        {
            Id = 20205;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(1);
        }
    }
}