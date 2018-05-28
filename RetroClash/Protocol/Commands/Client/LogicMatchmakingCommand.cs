using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Commands.Client
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

            /*if (enemy != null)
            {
                if (Device.Player.Battle == null)
                    Device.Player.Battle = new Battle(Device.Player);

                Device.Player.Battle.SetDefender(enemy);
            }*/

            if (Device.Player.Shield.IsShieldActive)
                Device.Player.Shield.RemoveShield();
        }
    }
}