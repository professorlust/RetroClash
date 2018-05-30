using RetroClashCore.Extensions;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicEditModeShownCommand : LogicCommand
    {
        public LogicEditModeShownCommand(Device device, Reader reader) : base(device, reader)
        {
        }
    }
}