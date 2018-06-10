using System;
using System.Collections.Generic;
using RetroRoyale.Protocol.Messages.Client;

namespace RetroRoyale.Protocol
{
    public class LogicMagicMessageFactory
    {
        public static Dictionary<int, Type> Messages;

        static LogicMagicMessageFactory()
        {
            Messages = new Dictionary<int, Type>
            {
                {10101, typeof(LoginMessage)},
                {10108, typeof(KeepAliveMessage)},
                {10113, typeof(SetDeviceTokenMessage)},
                {10118, typeof(AccountSwitchedMessage)},
                {14101, typeof(GoHomeMessage)},
                {14102, typeof(EndClientTurnMessage)},
                {14201, typeof(BindFacebookAccountMessage)},
                {14211, typeof(UnbindFacebookAccountMessage)},
            };
        }
    }
}