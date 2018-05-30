using System.Threading.Tasks;
using RetroClashCore.Helpers;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicNewsSeenCommand : LogicCommand
    {
        public LogicNewsSeenCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            Device.Player.LogicGameObjectManager.LastNewsSeen = Reader.ReadInt32();
        }
    }
}