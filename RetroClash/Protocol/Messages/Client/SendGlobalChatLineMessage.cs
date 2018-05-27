using System;
using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Logic.Manager.Items;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Protocol.Messages.Client
{
    public class SendGlobalChatLineMessage : PiranhaMessage
    {
        public SendGlobalChatLineMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public string Message { get; set; }

        public override void Decode()
        {
            Message = Reader.ReadString();
        }

        public override async Task Process()
        {
            if (Message.StartsWith("/"))
                switch (Message.Split(' ')[0])
                {
                    case "/help":
                    {
                        await Resources.Gateway.Send(new GlobalChatLineMessage(Device)
                        {
                            Message =
                                "Available commands:\n\n/help\n  -> List of all commands.\n/rename\n  -> Not happy with your name? Change it again!\n/prebase [level]\n  -> Load a premade base from level 1 to 6.\n/reset\n  -> Reset your village and start from beginning.\n/wall [level]\n  -> Set the level of all walls.",
                            Name = "DebugManager",
                            ExpLevel = 100,
                            League = 16,
                            AccountId = 0
                        });
                        break;
                    }

                    case "/prebase":
                    {
                        var lvl = Convert.ToInt32(Message.Split(' ')[1]);
                        if (lvl > 0 && lvl < 7)
                        {
                            Device.Player.LogicGameObjectManager.Json = Resources.Levels.Prebases[lvl - 1];

                            await Resources.Gateway.Send(new OwnHomeDataMessage(Device));

                            await Resources.Gateway.Send(new GlobalChatLineMessage(Device)
                            {
                                Message = $"Base {lvl} has been set.",
                                Name = "DebugManager",
                                ExpLevel = 100,
                                League = 16
                            });
                        }
                        break;
                    }

                    case "/reset":
                    {
                        Device.Player.LogicGameObjectManager.Json = Resources.Levels.StartingHome;
                        Device.Player.Achievements.Clear();
                        Device.Player.HeroManager.Clear();
                        Device.Player.Shield.RemoveShield();
                        Device.Player.Units.Spells.Clear();
                        Device.Player.Units.Troops.Clear();

                        await Resources.Gateway.Send(new OwnHomeDataMessage(Device));

                        await Resources.Gateway.Send(new GlobalChatLineMessage(Device)
                        {
                            Message = "Village has been set to default.",
                            Name = "DebugManager",
                            ExpLevel = 100,
                            League = 16
                        });

                        break;
                    }

                    case "/rename":
                    {
                        Device.Player.TutorialSteps = 10;
                        Device.Player.ExpLevel = 1;

                        await Resources.Gateway.Send(new OwnHomeDataMessage(Device));

                        break;
                    }

                    case "/wall":
                    {
                        var lvl = Convert.ToInt32(Message.Split(' ')[1]);
                        if (lvl > 0 && lvl < 12)
                        {
                            foreach (var building in Device.Player.LogicGameObjectManager.Buildings)
                            {
                                if (building.Data != 1000010) continue;
                                building.Level = lvl - 1;
                            }

                            await Resources.Gateway.Send(new OwnHomeDataMessage(Device));

                            await Resources.Gateway.Send(new GlobalChatLineMessage(Device)
                            {
                                Message = $"Wall level set to {lvl}.",
                                Name = "DebugManager",
                                ExpLevel = 100,
                                League = 16
                            });
                        }
                        break;
                    }

                    default:
                    {
                        await Resources.Gateway.Send(new GlobalChatLineMessage(Device)
                        {
                            Message = "Invalid Command. Type '/help' for a list of all commands.",
                            Name = "DebugManager",
                            ExpLevel = 100,
                            League = 16
                        });
                        break;
                    }
                }
            else if ((DateTime.Now - Device.Player.LastChatMessage).TotalSeconds >= 1.0)
                if (!string.IsNullOrEmpty(Message))
                {
                    await Resources.ChatManager.Process(new GlobalChatEntry
                    {
                        Message = Message,
                        SenderName = Device.Player.Name,
                        SenderId = Device.Player.AccountId,
                        SenderExpLevel = Device.Player.ExpLevel,
                        SenderLeague = LogicUtils.GetLeagueByScore(Device.Player.Score)
                    });

                    Device.Player.LastChatMessage = DateTime.Now;
                }
        }
    }
}