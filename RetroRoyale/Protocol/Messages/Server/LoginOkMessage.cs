using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Messages.Server
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
            await Stream.WriteLong(Device.Player.AccountId); // Account Id
            await Stream.WriteLong(Device.Player.AccountId); // Home Id
            await Stream.WriteString(Device.Player.PassToken); // Pass Token

            await Stream.WriteString(Device.Player.FacebookId); // Facebook Id
            await Stream.WriteString(null); // Gamecenter Id

            await Stream.WriteInt(0); // Server Major
            await Stream.WriteInt(0); // Server Build
            await Stream.WriteInt(0); // Content Version

            await Stream.WriteString("integration"); // Server Env

            await Stream.WriteString("171514200197498");
            await Stream.WriteString("DE");
            await Stream.WriteString("Berlin");

            await Stream.WriteString(Resources.Configuration.PatchUrl);

            await Stream.WriteString("https://event-assets.clashroyale.com"); // Facebook App Id   
            await Stream.WriteVInt(3);
        }
    }
}