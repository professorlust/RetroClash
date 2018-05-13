using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicBuyBuilding : Command
    {
        public LogicBuyBuilding(Device device, Reader reader) : base(device, reader)
        {
        }

        public int BuildingId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override void Decode()
        {
            X = Reader.ReadInt32();
            Y = Reader.ReadInt32();
            BuildingId = Reader.ReadInt32();
            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            var id = Device.Player.LogicGameObjectManager.AddBuilding(BuildingId, X, Y);

            switch (BuildingId)
            {
                case 1000022:
                    Device.Player.HeroManager.Add(28000000, id); // Barbarian King
                    break;
                case 1000025:
                    Device.Player.HeroManager.Add(28000001, id); // Archer Queen
                    break;
            }
        }
    }
}