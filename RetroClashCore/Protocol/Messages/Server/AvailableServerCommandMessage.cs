using System.Threading.Tasks;
using RetroClashCore.Helpers;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class AvailableServerCommandMessage : PiranhaMessage
    {
        public AvailableServerCommandMessage(Device device) : base(device)
        {
            Id = 24111;
        }

        public LogicCommand Command { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(Command.Type);
            await Stream.WriteBufferAsync(Command.Stream.ToArray());
        }
    }
}