using System.Threading.Tasks;
using RetroClashCore.Helpers;
using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class PersonalBreakStartedMessage : PiranhaMessage
    {
        public PersonalBreakStartedMessage(Device device) : base(device)
        {
            Id = 20171;
        }

        public override async Task Encode()
        {   
            await Stream.WriteIntAsync(10); // Seconds?
        }
    }
}