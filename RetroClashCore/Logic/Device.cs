using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using RetroClashCore.Crypto;
using RetroClashCore.Helpers;
using RetroClashCore.Network;
using RetroClashCore.Protocol;
using RetroClashCore.Protocol.Messages.Server;

namespace RetroClashCore.Logic
{
    public class Device : IDisposable
    {
        public Rc4Core Rc4 = new Rc4Core();
        public Enums.State State = Enums.State.Login;

        public Device(UserToken userToken)
        {
            UserToken = userToken;
            SessionId = Guid.NewGuid();
        }

        public UserToken UserToken { get; set; }
        public Player Player { get; set; }
        public Guid SessionId { get; set; }

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

                    if (identifier >= 10000 && identifier <= 30000)
                    {
                        if (!LogicMagicMessageFactory.Messages.ContainsKey(identifier))
                        {
                            if (Configuration.Debug)
                                Disconnect();

                            Logger.Log($"PACKET {identifier} is not known.", Enums.LogType.Warning);
                        }
                        else
                        {
                            if (Activator.CreateInstance(LogicMagicMessageFactory.Messages[identifier], this, reader) is PiranhaMessage
                                message)
                                try
                                {
                                    message.Id = identifier;
                                    message.Length = length;
                                    message.Version = reader.ReadUInt16();

                                    message.Decrypt();
                                    message.Decode();

                                    /*if (Configuration.Debug)
                                        Console.WriteLine(message.ToString());*/

                                    await message.Process();

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

        public async void Disconnect()
        {
            try
            {
                if (UserToken.EventArgs.AcceptSocket == null) return;
                await Resources.Gateway.Send(new OutOfSyncMessage(this));

                Resources.Gateway.DissolveSocket(UserToken.Socket);
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
        }
    }
}