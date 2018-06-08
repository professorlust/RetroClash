using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using RetroClashCore.Database;
using RetroClashCore.Logic.Battle;
using RetroClashCore.Logic.Manager;
using RetroClashCore.Logic.Replay.Items;
using RetroClashCore.Logic.StreamEntry;
using RetroGames.Helpers;

namespace RetroClashCore.Logic
{
    public class Player : IDisposable
    {
        [JsonProperty("achievements")] public Achievements Achievements = new Achievements();

        [JsonProperty("heroes")] public LogicHeroManager HeroManager = new LogicHeroManager();

        [JsonIgnore] public LogicGameObjectManager LogicGameObjectManager = new LogicGameObjectManager();

        [JsonProperty("resources")] public LogicResourcesManager ResourcesManager = new LogicResourcesManager();

        [JsonProperty("shield")] public LogicShield Shield = new LogicShield();

        [JsonProperty("stream")] public List<AvatarStreamEntry> Stream = new List<AvatarStreamEntry>(20);

        [JsonIgnore] public Timer Timer = new Timer(5000)
        {
            AutoReset = true
        };

        [JsonProperty("units")] public Units Units = new Units();

        public Player(long id, string token)
        {
            AccountId = id;

            Name = "RetroClash";
            PassToken = token;
            ExpLevel = 1;
            TutorialSteps = 10;
            Language = "EN";
            Diamonds = 1000000000;

            ResourcesManager.Initialize();
            LogicGameObjectManager.Json = Resources.Levels.StartingHome;
        }

        [JsonProperty("account_id")]
        public long AccountId { get; set; }

        [JsonProperty("alliance_id")]
        public long AllianceId { get; set; }

        [JsonProperty("fb_id")]
        public string FacebookId { get; set; }

        [JsonProperty("account_name")]
        public string Name { get; set; }

        [JsonProperty("device_name")]
        public string DeviceName { get; set; }

        [JsonProperty("pass_token")]
        public string PassToken { get; set; }

        [JsonProperty("exp_level")]
        public int ExpLevel { get; set; }

        [JsonProperty("exp_points")]
        public int ExpPoints { get; set; }

        [JsonProperty("tutorial_steps")]
        public int TutorialSteps { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonIgnore]
        public Device Device { get; set; }

        [JsonIgnore]
        public PvbBattle Battle { get; set; }

        [JsonProperty("diamonds")]
        public int Diamonds { get; set; }

        public void Dispose()
        {
            Timer.Stop();

            Timer = null;
            Device = null;
            Units = null;
            Battle = null;
            Stream = null;
            HeroManager = null;
            Achievements = null;
            ResourcesManager = null;
            LogicGameObjectManager = null;
        }

        public void AddEntry(AvatarStreamEntry entry)
        {
            while (Stream.Count >= 20)
                Stream.RemoveAt(0);

            entry.Id = (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            Stream.Add(entry);
        }

        public async Task LogicClientHome(MemoryStream stream)
        {
            await stream.WriteIntAsync(0);

            await stream.WriteLongAsync(AccountId); // Account Id

            await stream.WriteStringAsync(LogicGameObjectManager.Json);

            await stream.WriteIntAsync(Shield.ShieldSecondsLeft);
            await stream.WriteIntAsync(0);
            await stream.WriteIntAsync(0);
        }

        public async Task LogicClientAvatar(MemoryStream stream)
        {
            await stream.WriteIntAsync(0);
            await stream.WriteLongAsync(AccountId); // Account Id
            await stream.WriteLongAsync(AccountId); // Home Id

            if (AllianceId > 0)
            {
                var alliance = await Resources.AllianceCache.GetAlliance(AllianceId);

                if (alliance != null)
                {
                    stream.WriteBool(true);
                    await stream.WriteLongAsync(AllianceId); // Alliance Id
                    await stream.WriteStringAsync(alliance.Name); // Alliance Name
                    await stream.WriteIntAsync(alliance.Badge); // Alliance Badge
                    await stream.WriteIntAsync(alliance.Members.Find(x => x.AccountId == AccountId)
                        .Role); // Alliance Role

                    stream.WriteByte(1);
                    await stream.WriteLongAsync(AllianceId); // Alliance Id

                    stream.WriteByte(1);
                    await stream.WriteLongAsync(AllianceId); // Alliance Id
                }
                else
                {
                    stream.WriteBool(false);
                }
            }
            else
            {
                stream.WriteBool(false);
            }

            await stream.WriteIntAsync(LogicUtils.GetLeagueByScore(Score)); // League Type

            await stream.WriteIntAsync(0); // Alliance Castle Level
            await stream.WriteIntAsync(10); // Alliance Total Capacity
            await stream.WriteIntAsync(0); // Alliance Used Capacity
            await stream.WriteIntAsync(LogicGameObjectManager?.GetTownHallLevel() ?? 1); // Townhall Level

            await stream.WriteStringAsync(Name); // Name
            await stream.WriteStringAsync(null); // Facebook Id 

            await stream.WriteIntAsync(ExpLevel); // Exp Level
            await stream.WriteIntAsync(ExpPoints); // Exp Points

            await stream.WriteIntAsync(Diamonds); // Diamonts
            await stream.WriteIntAsync(0); // Current Diamonts
            await stream.WriteIntAsync(0); // Free Diamonts

            await stream.WriteIntAsync(0); // Unknown

            await stream.WriteIntAsync(Score); // Score

            await stream.WriteIntAsync(0); // Attack Win Count
            await stream.WriteIntAsync(0); // Attack Lose Count

            await stream.WriteIntAsync(0); // Defense Win Count
            await stream.WriteIntAsync(0); // Defense Lose Count

            stream.WriteBool(false); // Name Set By User 
            await stream.WriteIntAsync(0);

            // Resource Caps
            await stream.WriteIntAsync(0);

            // Resources
            await stream.WriteIntAsync(ResourcesManager.Count);
            foreach (var resource in ResourcesManager)
            {
                await stream.WriteIntAsync(resource.Id);
                await stream.WriteIntAsync(resource.Value);
            }

            // Troops
            await stream.WriteIntAsync(Units.Troops.Count);
            foreach (var troop in Units.Troops)
            {
                await stream.WriteIntAsync(troop.Id);
                await stream.WriteIntAsync(troop.Count);
            }

            // Spells
            await stream.WriteIntAsync(Units.Spells.Count);
            foreach (var spell in Units.Spells)
            {
                await stream.WriteIntAsync(spell.Id);
                await stream.WriteIntAsync(spell.Count);
            }

            // Troop Levels
            await stream.WriteIntAsync(Units.Troops.Count);
            foreach (var troop in Units.Troops)
            {
                await stream.WriteIntAsync(troop.Id);
                await stream.WriteIntAsync(troop.Level);
            }

            // Spell Levels
            await stream.WriteIntAsync(Units.Spells.Count);
            foreach (var spell in Units.Spells)
            {
                await stream.WriteIntAsync(spell.Id);
                await stream.WriteIntAsync(spell.Level);
            }

            // Hero Levels
            await stream.WriteIntAsync(HeroManager.Count);
            foreach (var hero in HeroManager)
            {
                await stream.WriteIntAsync(hero.Id);
                await stream.WriteIntAsync(hero.Level);
            }

            // Hero Health
            await stream.WriteIntAsync(HeroManager.Count);
            foreach (var hero in HeroManager)
            {
                await stream.WriteIntAsync(hero.Id);
                await stream.WriteIntAsync(hero.Health);
            }

            // Hero State
            await stream.WriteIntAsync(HeroManager.Count);
            foreach (var hero in HeroManager)
            {
                await stream.WriteIntAsync(hero.Id);
                await stream.WriteIntAsync(hero.State);
            }

            // Alliance Units
            await stream.WriteIntAsync(0);

            // Tutorials
            await stream.WriteIntAsync(TutorialSteps);
            for (var index = 21000000; index < 21000000 + TutorialSteps; index++)
                await stream.WriteIntAsync(index);

            // Achievements
            await stream.WriteIntAsync(Achievements.Count);
            foreach (var achievement in Achievements)
                await stream.WriteIntAsync(achievement.Id);

            // Achievement Progress
            await stream.WriteIntAsync(Achievements.Count);
            foreach (var achievement in Achievements)
            {
                await stream.WriteIntAsync(achievement.Id);
                await stream.WriteIntAsync(achievement.Data);
            }

            // Npc Map Progress
            await stream.WriteIntAsync(50);
            for (var index = 17000000; index < 17000050; index++)
            {
                await stream.WriteIntAsync(index);
                await stream.WriteIntAsync(3);
            }

            await stream.WriteIntAsync(0); // Npc Looted Gold DataSlot
            await stream.WriteIntAsync(0); // Npc Looted Elixir DataSlot
        }

        public async Task AvatarRankingEntry(MemoryStream stream)
        {
            await stream.WriteIntAsync(ExpLevel); // Exp Level
            await stream.WriteIntAsync(0); // Attack Win Count
            await stream.WriteIntAsync(0); // Attack Lose Count
            await stream.WriteIntAsync(0); // Defense Win Count
            await stream.WriteIntAsync(0); // Defense Lose Count
            await stream.WriteIntAsync(LogicUtils.GetLeagueByScore(Score)); // League Type

            await stream.WriteStringAsync(Language); // Country
            await stream.WriteLongAsync(AccountId); // Home Id

            if (AllianceId > 0)
            {
                var alliance = await Resources.AllianceCache.GetAlliance(AllianceId);

                if (alliance != null)
                {
                    stream.WriteBool(true);
                    await stream.WriteLongAsync(AllianceId); // Clan Id
                    await stream.WriteStringAsync(alliance.Name); // Clan Name
                    await stream.WriteIntAsync(alliance.Badge); // Badge
                }
                else
                {
                    AllianceId = 0;
                    stream.WriteBool(false);
                }
            }
            else
            {
                stream.WriteBool(false);
            }
        }

        public async void SaveCallback(object state, ElapsedEventArgs args)
        {
            if (Redis.IsConnected)
                await Redis.CachePlayer(this);

            if (Device.TimeSinceLastKeepAlive <= 40) return;

            if (Device.State != Enums.State.Login)
                Device.Disconnect();
        }

        public void AddDiamonds(int value)
        {
            Diamonds += value;
        }

        public bool UseDiamonds(int value)
        {
            if (Diamonds < value)
                return false;

            Diamonds -= value;

            return true;
        }

        public ReplayProfile GetReplayProfile(bool attacker)
        {
            var profile = new ReplayProfile
            {
                Name = Name,
                Score = Score,
                League = LogicUtils.GetLeagueByScore(Score),
                TownHallLevel = LogicGameObjectManager.GetTownHallLevel(),
                CastleLevel = 1,
                CastleTotal = 15,
                CastleUsed = 0,
                BadgeId = 13000000,
                AllianceName = "RetroClash"
            };

            foreach (var troop in Units.Troops)
                if (troop.Count > 0)
                    profile.Units.Add(new ReplayUnitItem
                    {
                        Id = troop.Id,
                        Cnt = troop.Count
                    });

            foreach (var spell in Units.Spells)
                if (spell.Count > 0)
                    profile.Spells.Add(new ReplayUnitItem
                    {
                        Id = spell.Id,
                        Cnt = spell.Count
                    });

            foreach (var unit in Units.Troops)
                profile.UnitUpgrades.Add(new ReplayUnitItem
                {
                    Id = unit.Id,
                    Cnt = unit.Level
                });

            foreach (var spell in Units.Spells)
                profile.SpellUpgrades.Add(new ReplayUnitItem
                {
                    Id = spell.Id,
                    Cnt = spell.Level
                });

            if (attacker)
                foreach (var resource in ResourcesManager)
                    profile.Resources.Add(new ReplayUnitItem
                    {
                        Id = resource.Id,
                        Cnt = resource.Value
                    });

            foreach (var hero in HeroManager)
                profile.HeroStates.Add(new ReplayUnitItem
                {
                    Id = hero.Id,
                    Cnt = hero.State
                });

            foreach (var hero in HeroManager)
                profile.HeroHealth.Add(new ReplayUnitItem
                {
                    Id = hero.Id,
                    Cnt = hero.Health
                });

            foreach (var hero in HeroManager)
                profile.HeroUpgrade.Add(new ReplayUnitItem
                {
                    Id = hero.Id,
                    Cnt = hero.Level
                });

            return profile;
        }
    }
}