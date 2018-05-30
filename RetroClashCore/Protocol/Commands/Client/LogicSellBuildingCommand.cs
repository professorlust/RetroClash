using System.Threading.Tasks;
using RetroClashCore.Extensions;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicSellBuildingCommand : LogicCommand
    {
        public LogicSellBuildingCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public int BuildingId { get; set; }

        public override void Decode()
        {
            BuildingId = Reader.ReadInt32();
            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            Device.Player.LogicGameObjectManager.RemoveDeco(BuildingId);
        }
    }
}