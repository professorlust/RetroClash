﻿using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroClashCore.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Client
{
    public class AskForJoinableAlliancesListMessage : PiranhaMessage
    {
        public AskForJoinableAlliancesListMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new JoinableAllianceListMessage(Device));
        }
    }
}