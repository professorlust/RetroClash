﻿using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class FacebookAccountAlreadyBoundMessage : PiranhaMessage
    {
        public FacebookAccountAlreadyBoundMessage(Device device) : base(device)
        {
            Id = 24262;
        }

        public Player Player { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteStringAsync(Player.FacebookId);

            Stream.WriteByte(1);

            await Stream.WriteLongAsync(Player.AccountId);
            await Stream.WriteStringAsync(Player.PassToken);

            await Player.LogicClientAvatar(Stream);
        }
    }
}