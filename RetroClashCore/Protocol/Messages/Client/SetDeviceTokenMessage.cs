using RetroClashCore.Extensions;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Messages.Client
{
    public class SetDeviceTokenMessage : PiranhaMessage
    {
        public SetDeviceTokenMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public override void Decode()
        {
            Reader.ReadString();
        }
    }
}