using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class HomeBattleReplayMessage : PiranhaMessage
    {
        public HomeBattleReplayMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public long ReplayId { get; set; }

        public override void Decode()
        {
            ReplayId = Reader.ReadInt64();
            Reader.ReadInt32(); // ShardId
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new HomeBattleReplayFailedMessage(Device));
        }
    }
}