using RetroClashCore.Extensions;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicEndCombatCommand : LogicCommand
    {
        public LogicEndCombatCommand(Device device, Reader reader) : base(device, reader)
        {
        }
    }
}