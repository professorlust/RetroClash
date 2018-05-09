﻿using System;
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
                {500, typeof(LogicBuyBuilding)},
                {501, typeof(LogicMoveBuilding)},
                {502, typeof(LogicUpgradeBuilding)},
                {503, typeof(LogicSellBuilding)},
                {504, typeof(LogicSpeedUpConstruction)},
                {507, typeof(LogicClearObstacle)},
                {508, typeof(LogicTrainUnit)},
                {510, typeof(LogicBuyTrap)},
                {512, typeof(LogicBuyDeco)},
                {516, typeof(LogicUnitUpgrade)},
                {517, typeof(LogicSpeedUpUnitUpgrade)},
                {520, typeof(LogicUnlockBuilding)},
                {521, typeof(LogicFreeWorker)},
                {522, typeof(LogicBuyShield)},
                //{523, typeof(LogicClaimAchievementReward)},
                {527, typeof(LogicUpgradeHero)},
                {528, typeof(LogicSpeedUpHeroUpgrade)},
                {529, typeof(LogicToggleHeroSleep)},
                {530, typeof(LogicSpeedUpHeroHealth)},
                {532, typeof(LogicNewShopItemsSeen)},
                {533, typeof(LogicMoveMultipleBuildings)},
                {534, typeof(LogicDisbandLeague)},
                {538, typeof(LogicLeagueNotificationsSeen)},
                {539, typeof(LogicNewsSeen)},
                {544, typeof(LogicEditModeShown)},
                {600, typeof(LogicPlaceAttacker)},
                {603, typeof(LogicEndCombat)},
                {604, typeof(LogicCastSpell)},
                {605, typeof(LogicPlaceHero)},
                {700, typeof(LogicMatchmaking)},
                {701, typeof(LogicCommandFailed)}
            };
        }
    }
}