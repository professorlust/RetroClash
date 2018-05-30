using System.Threading.Tasks;
using RetroClashCore.Extensions;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicCommandFailed : LogicCommand
    {
        public LogicCommandFailed(Device device, Reader reader) : base(device, reader)
        {
        }

        public int FailedCommandType { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(0); // FailedCommandType
        }
    }
}