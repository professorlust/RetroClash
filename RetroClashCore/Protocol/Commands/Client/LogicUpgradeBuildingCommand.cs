﻿using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicUpgradeBuildingCommand : LogicCommand
    {
        public LogicUpgradeBuildingCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public int BuildingId { get; set; }

        public override void Decode()
        {
            BuildingId = Reader.ReadInt32();
            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            await Task.Run(() => { Device.Player.LogicGameObjectManager.Upgrade(BuildingId); });
        }
    }
}