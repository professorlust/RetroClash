using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Client
{
    public class AccountSwitchedMessage : Message
    {
        public AccountSwitchedMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public long SwitchedToAccountId { get; set; }

        public override void Decode()
        {
            SwitchedToAccountId = Reader.ReadInt64();
        }
    }
}