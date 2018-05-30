using System.Threading.Tasks;
using RetroClashCore.Helpers;
using RetroClashCore.Logic;
using RetroClashCore.Protocol.Messages.Server;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicMatchmakingCommand : LogicCommand
    {
        public LogicMatchmakingCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            var enemy = Resources.PlayerCache.Random;

            await Resources.Gateway.Send(new EnemyHomeDataMessage(Device)
            {
                Enemy = enemy
            });

            if (enemy != null && Device.State == Enums.State.Battle)
            {
                if (Device.Player.Battle == null)
                    Device.Player.Battle = new Battle(Device.Player);

                Device.Player.Battle.SetDefender(enemy);
            }

            if (Device.Player.Shield.IsShieldActive)
                Device.Player.Shield.RemoveShield();
        }
    }
}