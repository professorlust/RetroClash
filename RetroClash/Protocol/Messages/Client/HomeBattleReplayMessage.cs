using System.Threading.Tasks;
using RetroClash.Database;
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
            var replay = await MySQL.GetReplay(ReplayId);

            if (replay != null)
            {
                await Resources.Gateway.Send(new HomeBattleReplayDataMessage(Device)
                {
                    Replay = replay
                });
            }
            else
            {
                await Resources.Gateway.Send(new HomeBattleReplayFailedMessage(Device));
            }
        }
    }
}