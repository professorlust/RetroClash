using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class FacebookAccountUnboundMessage : Message
    {
        public FacebookAccountUnboundMessage(Device device) : base(device)
        {
            Id = 24214;
        }
    }
}