﻿using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class OwnHomeDataMessage : PiranhaMessage
    {
        public OwnHomeDataMessage(Device device) : base(device)
        {
            Id = 24101;
            Device.State = Enums.State.Home;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(0);

            await Device.Player.LogicClientHome(Stream);

            await Device.Player.LogicClientAvatar(Stream);
        }
    }
}