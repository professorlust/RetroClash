using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicPlaceHeroCommand : Command
    {
        public LogicPlaceHeroCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public int HeroId { get; set; }

        public override void Decode()
        {
            Reader.ReadInt32();
            Reader.ReadInt32();

            HeroId = Reader.ReadInt32();

            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            var hero = Device.Player.HeroManager.GetByType(HeroId);

            if (hero != null)
                hero.Health = 60;
        }
    }
}