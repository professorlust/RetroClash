using System.Threading.Tasks;
using RetroClashCore.Helpers;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicToggleHeroSleepCommand : LogicCommand
    {
        public LogicToggleHeroSleepCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public int HeroId { get; set; }

        public override void Decode()
        {
            HeroId = Reader.ReadInt32();

            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            var hero = Device.Player.HeroManager.Get(HeroId);

            hero?.Toogle();
        }
    }
}