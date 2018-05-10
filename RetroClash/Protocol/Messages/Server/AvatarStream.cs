using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class AvatarStream : Message
    {
        public AvatarStream(Device device) : base(device)
        {
            Id = 24411;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(0); // StreamEntryCount
        }
    }
}