using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class LoginOkMessage : PiranhaMessage
    {
        public LoginOkMessage(Device device) : base(device)
        {
            Id = 20104;
            Version = 1;
        }

        public override async Task Encode()
        {
            await Stream.WriteLongAsync(Device.Player.AccountId); // Account Id
            await Stream.WriteLongAsync(Device.Player.AccountId); // Home Id
            await Stream.WriteStringAsync(Device.Player.PassToken); // Pass Token

            await Stream.WriteStringAsync(Device.Player.FacebookId); // Facebook Id
            await Stream.WriteStringAsync(null); // Gamecenter Id

            await Stream.WriteIntAsync(5); // Server Major
            await Stream.WriteIntAsync(0); // Server Build
            await Stream.WriteIntAsync(0); // Content Version

            await Stream.WriteStringAsync("integration"); // Server Env

            await Stream.WriteIntAsync(0); // Session Count
            await Stream.WriteIntAsync(0); // PlayTimeSeconds
            await Stream.WriteIntAsync(0); // DaysSinceStartedPlaying

            await Stream.WriteStringAsync("171514200197498"); // Facebook App Id       
        }
    }
}