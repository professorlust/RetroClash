using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Logic.Manager;
using RetroClash.Logic.Manager.Items.Replay;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicPlaceAttackerCommand : LogicCommand
    {
        public LogicPlaceAttackerCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public int UnitId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override void Decode()
        {
            X = Reader.ReadInt32(); // X
            Y = Reader.ReadInt32(); // Y

            UnitId = Reader.ReadInt32();

            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            var index = Device.Player.Units.Troops.FindIndex(unit => unit.Id == UnitId);

            if (index > -1)
            {
                Device.Player.Units.Troops[index].Count--;

                /*Device.Player.Battle.RecordCommand(new ReplayCommand
                {
                    CommandType = Type,
                    ReplayCommandInfo = new ReplayCommandInfo
                    {
                        ReplayCommandBase = GetBase(),
                        X = X,
                        Y = Y,
                        Data = UnitId
                    }
                });*/
            }
        }
    }
}