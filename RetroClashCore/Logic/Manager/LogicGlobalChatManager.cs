using System.Threading.Tasks;
using RetroClashCore.Logic.Manager.Items;
using RetroClashCore.Protocol.Messages.Server;

namespace RetroClashCore.Logic.Manager
{
    public class LogicGlobalChatManager
    {
        public async Task Process(GlobalChatEntry entry)
        {
            foreach (var player in Resources.PlayerCache.Values)
                await Resources.Gateway.Send(new GlobalChatLineMessage(player.Device)
                {
                    AccountId = entry.SenderId,
                    Message = entry.Message,
                    ExpLevel = entry.SenderExpLevel,
                    League = entry.SenderLeague,
                    Name = entry.SenderName
                });
        }
    }
}