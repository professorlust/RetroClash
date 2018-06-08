using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Commands.Server
{
    public class LogicDiamondsAdded : LogicCommand
    {
        public LogicDiamondsAdded(Device device) : base(device)
        {
            Type = 7;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(0); // Free Diamonds
            await Stream.WriteIntAsync(0); // Ammount
            await Stream.WriteStringAsync("G:0"); // TransactionId
        }
    }
}