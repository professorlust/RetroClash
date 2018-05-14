using System;
using System.Collections.Generic;
using RetroClash.Protocol.Messages.Client;

namespace RetroClash.Protocol
{
    public class MessageFactory
    {
        public static Dictionary<int, Type> Messages;

        public MessageFactory()
        {
            Messages = new Dictionary<int, Type>
            {
                {10101, typeof(LoginMessage)},
                {10108, typeof(KeepAliveMessage)},
                {10113, typeof(SetDeviceTokenMessage)},
                {10212, typeof(ChangeAvatarNameMessage)},
                {14101, typeof(GoHomeMessage)},
                {14102, typeof(EndClientTurnMessage)},
                {14113, typeof(VisitHomeMessage)},
                {14134, typeof(AttackNpcMessage)},
                {14262, typeof(BindGoogleServiceAccountMessage)},
                {14301, typeof(CreateAllianceMessage)},
                {14302, typeof(AskForAllianceDataMessage)},
                {14303, typeof(AskForJoinableAlliancesListMessage)},
                {14305, typeof(JoinAllianceMessage)},
                {14315, typeof(ChatToAllianceStreamMessage)},
                {14325, typeof(AskForAvatarProfileMessage)},
                {14401, typeof(AskForAllianceRankingListMessage)},
                {14403, typeof(AskForAvatarRankingListMessage)},
                {14404, typeof(AskForAvatarLocalRankingListMessage)},
                {14715, typeof(SendGlobalChatLineMessage)}
            };
        }
    }
}