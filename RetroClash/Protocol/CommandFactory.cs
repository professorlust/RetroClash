using System;
using System.Collections.Generic;
using RetroClash.Protocol.Commands.Client;

namespace RetroClash.Protocol
{
    public class CommandFactory
    {
        public static Dictionary<int, Type> Commands;

        public CommandFactory()
        {
            Commands = new Dictionary<int, Type>
            {
                {500, typeof(LogicBuyBuildingCommand)},
                {501, typeof(LogicMoveBuildingCommand)},
                {502, typeof(LogicUpgradeBuildingCommand)},
                {503, typeof(LogicSellBuildingCommand)},
                {504, typeof(LogicSpeedUpConstructionCommand)},
                {507, typeof(LogicClearObstacleCommand)},
                {508, typeof(LogicTrainUnitCommand)},
                {510, typeof(LogicBuyTrapCommand)},
                {512, typeof(LogicBuyDecoCommand)},
                {516, typeof(LogicUnitUpgradeCommand)},
                {517, typeof(LogicSpeedUpUnitUpgradeCommand)},
                {520, typeof(LogicUnlockBuildingCommand)},
                {521, typeof(LogicFreeWorkerCommand)},
                {522, typeof(LogicBuyShieldCommand)},
                {523, typeof(LogicClaimAchievementRewardCommand)},
                {526, typeof(LogicBoostBuildingCommand)},
                {527, typeof(LogicUpgradeHeroCommand)},
                {528, typeof(LogicSpeedUpHeroUpgradeCommand)},
                {529, typeof(LogicToggleHeroSleepCommand)},
                {530, typeof(LogicSpeedUpHeroHealthCommand)},
                {532, typeof(LogicNewShopItemsSeenCommand)},
                {533, typeof(LogicMoveMultipleBuildingsCommand)},
                {534, typeof(LogicDisbandLeagueCommand)},
                {538, typeof(LogicLeagueNotificationsSeenCommand)},
                {539, typeof(LogicNewsSeenCommand)},
                {544, typeof(LogicEditModeShownCommand)},
                {600, typeof(LogicPlaceAttackerCommand)},
                {603, typeof(LogicEndCombatCommand)},
                {604, typeof(LogicCastSpellCommand)},
                {605, typeof(LogicPlaceHeroCommand)},
                {700, typeof(LogicMatchmakingCommand)},
                {701, typeof(LogicCommandFailed)}
            };
        }
    }
}