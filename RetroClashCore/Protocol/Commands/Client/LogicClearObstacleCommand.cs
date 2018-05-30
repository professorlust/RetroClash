using System.Threading.Tasks;
using RetroClashCore.Extensions;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicClearObstacleCommand : LogicCommand
    {
        public LogicClearObstacleCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public int ObstacleId { get; set; }

        public override void Decode()
        {
            ObstacleId = Reader.ReadInt32();
            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            Device.Player.LogicGameObjectManager.RemoveObstacle(ObstacleId);
        }
    }
}