﻿using System.Threading.Tasks;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class AllianceDataMessage : PiranhaMessage
    {
        public AllianceDataMessage(Device device) : base(device)
        {
            Id = 24301;
        }

        public long AllianceId { get; set; }

        public override async Task Encode()
        {
            var alliance = await Resources.AllianceCache.GetAlliance(AllianceId);

            await alliance.AllianceFullEntry(Stream);
        }
    }
}