using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
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
            await Stream.WriteIntAsync(Command.Id);
            await Stream.WriteBufferAsync(Command.Stream.ToArray());
        }
    }
}