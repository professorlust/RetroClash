using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class AvatarStreamMessage : PiranhaMessage
    {
        public AvatarStreamMessage(Device device) : base(device)
        {
            Id = 24411;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(Device.Player.Stream.Count); // StreamEntryCount

            foreach (var entry in Device.Player.Stream)
            {
                await entry.Encode(Stream);
            }
        }
    }
}