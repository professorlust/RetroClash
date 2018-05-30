using RetroClashCore.Extensions;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicSpeedUpUnitUpgradeCommand : LogicCommand
    {
        public LogicSpeedUpUnitUpgradeCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public override void Decode()
        {
            Reader.ReadInt32();
            Reader.ReadInt32();
        }
    }
}