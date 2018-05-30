using System.Threading.Tasks;
using RetroClashCore.Helpers;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicLeagueNotificationsSeenCommand : LogicCommand
    {
        public LogicLeagueNotificationsSeenCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            Device.Player.LogicGameObjectManager.LastLeagueRank = Reader.ReadInt32();
        }
    }
}