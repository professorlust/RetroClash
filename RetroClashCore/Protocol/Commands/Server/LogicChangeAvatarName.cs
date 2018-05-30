using System.Threading.Tasks;
using RetroClashCore.Extensions;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Commands.Server
{
    public class LogicChangeAvatarName : LogicCommand
    {
        public LogicChangeAvatarName(Device device) : base(device)
        {
            Type = 3;
        }

        public string AvatarName { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteStringAsync(AvatarName);
        }
    }
}