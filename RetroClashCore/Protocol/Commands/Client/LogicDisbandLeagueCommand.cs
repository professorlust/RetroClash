using RetroClashCore.Extensions;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicDisbandLeagueCommand : LogicCommand
    {
        public LogicDisbandLeagueCommand(Device device, Reader reader) : base(device, reader)
        {
        }
    }
}