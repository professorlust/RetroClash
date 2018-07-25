using System;
using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Client
{
    public class KeepAliveMessage : PiranhaMessage
    {
        public KeepAliveMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            Device.LastKeepAlive = DateTime.UtcNow;
        }
    }
}