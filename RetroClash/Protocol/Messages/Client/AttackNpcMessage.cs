using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class AttackNpcMessage : Message
    {
        public AttackNpcMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public int LevelId { get; set; }

        public override void Decode()
        {
            LevelId = Reader.ReadInt32();
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new NpcDataMessage(Device)
            {
                NpcId = LevelId
            });
        }
    }
}