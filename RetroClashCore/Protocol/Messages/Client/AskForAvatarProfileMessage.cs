using System.Threading.Tasks;
using RetroClashCore.Helpers;
using RetroClashCore.Logic;
using RetroClashCore.Protocol.Messages.Server;

namespace RetroClashCore.Protocol.Messages.Client
{
    public class AskForAvatarProfileMessage : PiranhaMessage
    {
        public AskForAvatarProfileMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public long AvatarId { get; set; }

        public override void Decode()
        {
            AvatarId = Reader.ReadInt64();
            Reader.ReadInt64();
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new AvatarProfileMessage(Device)
            {
                UserId = AvatarId
            });
        }
    }
}