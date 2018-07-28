using System;
using System.Threading.Tasks;
using RetroRoyale.Network;
using RetroRoyale.Protocol;
using RetroGames.Crypto.RC4;
using RetroGames.Helpers;

namespace RetroRoyale.Logic
{
    public class Device : IDisposable
    {
        public DateTime LastChatMessage = DateTime.UtcNow;
        public DateTime LastKeepAlive = DateTime.UtcNow;
        public DateTime LoginTime = DateTime.UtcNow;

        public Rc4Core Rc4 = new Rc4Core(Resources.Configuration.EncryptionKey, "nonce");
        public Enums.State State = Enums.State.Login; 

        public Device(UserToken userToken)
        {
            UserToken = userToken;
            SessionId = Guid.NewGuid();
        }

        public UserToken UserToken { get; set; }
        public Player Player { get; set; }
        public Guid SessionId { get; set; }

        public long TimeSinceLastKeepAlive => (long) DateTime.UtcNow.Subtract(LastKeepAlive).TotalSeconds;

        public int ServerTick => Utils.ToTick(DateTime.UtcNow.Subtract(LoginTime));

        public void Dispose()
        {
            Rc4 = null;
            UserToken = null;
            Player = null;
            SessionId = Guid.Empty;
        }

        public async Task ProcessPacket(byte[] buffer)
        {
            if (buffer.Length >= 7)
                using (var reader = new Reader(buffer))
                {
                    var identifier = reader.ReadUInt16();

                    var length = (ushort) reader.ReadUInt24();

                    if (buffer.Length - 7 < length) return;

                    if (identifier >= 10000 && identifier < 20000)
                    {
                        if (!LogicScrollMessageFactory.Messages.ContainsKey(identifier))
                        {
                            if (Configuration.Debug)
                                Disconnect();

                            Logger.Log($"PACKET {identifier} is not known.", Enums.LogType.Warning);
                        }
                        else
                        {
                            if (Activator.CreateInstance(LogicScrollMessageFactory.Messages[identifier], this, reader) is
                                PiranhaMessage
                                message)
                                try
                                {
                                    message.Id = identifier;
                                    message.Length = length;
                                    message.Version = reader.ReadUInt16();

                                    message.Decrypt();
                                    message.Decode();

                                    await message.Process();

                                    Logger.Log($"Message {identifier} has been handled.", Enums.LogType.Debug);

                                    if (State > Enums.State.Login && message.Save)
                                        await Player.Update();

                                    message.Dispose();
                                }
                                catch (Exception exception)
                                {
                                    Logger.Log(exception, Enums.LogType.Error);
                                }
                        }

                        if (buffer.Length - 7 - length >= 7)
                            await ProcessPacket(reader.ReadBytes(buffer.Length - 7 - length));
                        else
                            UserToken.Reset();
                    }
                    else
                    {
                        Disconnect();
                    }
                }
        }

        public void Disconnect()
        {
            try
            {
                if (UserToken?.Socket == null) return;
                Resources.Gateway.DissolveSocket(UserToken.Socket);
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
        }
    }
}