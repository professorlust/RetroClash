using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicEditModeShownCommand : Command
    {
        public LogicEditModeShownCommand(Device device, Reader reader) : base(device, reader)
        {
        }
    }
}