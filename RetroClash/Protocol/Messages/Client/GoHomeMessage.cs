using System.Threading.Tasks;
using RetroClash.Database;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class GoHomeMessage : PiranhaMessage
    {
        public GoHomeMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            /*if (Device.State == Enums.State.Battle)
            {
                if (Device.Player.Battle.Replay.Commands.Count > 0)
                {
                    var id = await MySQL.SaveReplay(Device.Player.Battle);

                    if (id > 0)
                    {
                        Device.Player.AddEntry(Device.Player.Battle.GetBattleReportStreamEntry(id));
                    }
                }

                Device.Player.Battle = null;
            }*/

            if (Device.State != Enums.State.Home)
            {
                await Resources.Gateway.Send(new OwnHomeDataMessage(Device));

                await Resources.Gateway.Send(new AvatarStreamMessage(Device));
            }
            else
                Device.Disconnect();
        }
    }
}