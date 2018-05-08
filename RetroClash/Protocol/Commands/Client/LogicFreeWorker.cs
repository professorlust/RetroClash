using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicFreeWorker : Command
    {
        public LogicFreeWorker(Device device, Reader reader) : base(device, reader)
        {           
        }

        public override void Decode()
        {
            Reader.ReadInt32();
            Reader.ReadByte();
        }
    }
}
