using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class AllianceListMessage : Message
    {
        public AllianceListMessage(Device device) : base(device)
        {
            Id = 24310;
        }
    }
}