using RetroClashCore.Helpers;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Messages.Client
{
    public class KeepAliveMessage : PiranhaMessage
    {
        public KeepAliveMessage(Device device, Reader reader) : base(device, reader)
        {
        }
    }
}