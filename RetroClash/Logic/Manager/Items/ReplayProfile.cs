using System.Collections.Generic;
using Newtonsoft.Json;

namespace RetroClash.Logic.Manager.Items
{
    public class ReplayProfile
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alliance_name")]
        public string AllianceName { get; set; }

        [JsonProperty("badge_id")]
        public int BadgeId { get; set; }

        [JsonProperty("league_type")]
        public int League { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("town_hall_lvl")]
        public int TownHallLevel { get; set; }

        [JsonProperty("units")]
        public List<ReplayUnitItem> Units { get; set; }

        [JsonProperty("spells")]
        public List<ReplayUnitItem> Spells { get; set; }

        [JsonProperty("unit_upgrades")]
        public List<ReplayUnitItem> UnitUpgrades { get; set; }

        [JsonProperty("spell_upgrades")]
        public List<ReplayUnitItem> Spellupgrades { get; set; }

        [JsonProperty("resources")]
        public List<ReplayUnitItem> Resources { get; set; }

        [JsonProperty("alliance_units")]
        public List<ReplayUnitItem> AllianceUnits { get; set; }

        [JsonProperty("hero_states")]
        public List<ReplayUnitItem> HeroStates { get; set; }

        [JsonProperty("hero_health")]
        public List<ReplayUnitItem> HeroHealth { get; set; }

        [JsonProperty("hero_upgrade")]
        public List<ReplayUnitItem> HeroUpgrade { get; set; }

        [JsonProperty("castle_lvl")]
        public int CastleLevel { get; set; }

        [JsonProperty("castle_total")]
        public int CastleTotal { get; set; }

        [JsonProperty("castle_used")]
        public int CastleUsed { get; set; }
    }
}