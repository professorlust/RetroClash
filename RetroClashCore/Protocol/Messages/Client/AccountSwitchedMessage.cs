using System.Threading.Tasks;
using RetroClashCore.Database;
using RetroClashCore.Helpers;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Messages.Client
{
    public class AccountSwitchedMessage : PiranhaMessage
    {
        public AccountSwitchedMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public long SwitchedToAccountId { get; set; }

        public override void Decode()
        {
            SwitchedToAccountId = Reader.ReadInt64();
        }

        public override async Task Process()
        {
            await MySQL.DeletePlayer(Device.Player.AccountId);
        }
    }
}