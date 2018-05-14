using System;
using System.Linq;
using System.Threading.Tasks;
using RetroClash.Database;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Logic.Slots;
using RetroClash.Logic.StreamEntry.Alliance;
using RetroClash.Protocol.Commands.Server;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class ChatToAllianceStreamMessage : Message
    {
        public ChatToAllianceStreamMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public string Message { get; set; }

        public override void Decode()
        {
            Message = Reader.ReadString();
        }

        public override async Task Process()
        {
            var alliance = await Resources.AllianceCache.GetAlliance(Device.Player.AllianceId);

            if (alliance != null)
            {
                var entry = new ChatStreamEntry
                {
                    CreationDateTime = DateTime.Now,
                    Id = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                    Message = Message
                };

                entry.SetSender(Device.Player);
                entry.SenderRole = alliance.GetRole(Device.Player.AccountId);

                alliance.AddEntry(entry);

                foreach (var member in alliance.Members)
                {
                    var player = await Resources.PlayerCache.GetPlayer(member.AccountId, true);

                    if (player != null)
                    {
                        await Resources.Gateway.Send(new AllianceStreamEntryMessage(player.Device)
                        {
                            AllianceStreamEntry = entry
                        });
                    }
                }
            }
        }
    }
}