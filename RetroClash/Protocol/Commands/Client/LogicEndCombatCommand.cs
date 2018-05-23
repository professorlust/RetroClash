using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicEndCombatCommand : Command
    {
        public LogicEndCombatCommand(Device device, Reader reader) : base(device, reader)
        {
        }
    }
}