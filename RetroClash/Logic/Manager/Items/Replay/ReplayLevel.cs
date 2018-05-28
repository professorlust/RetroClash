/*using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RetroClash.Logic.Manager.Items.Replay
{
    public class ReplayLevel
    {
        [JsonProperty("traps")] public List<Trap> Traps = new List<Trap>();

        [JsonProperty("decos")] public List<Decoration> Decorations = new List<Decoration>();

        [JsonProperty("obstacles")] public List<Obstacle> Obstacles = new List<Obstacle>();

        [JsonProperty("buildings")] public List<Building> Buildings = new List<Building>();

        public ReplayLevel(LogicGameObjectManager logicGameObjectManager)
        {
            try
            {
                Traps = logicGameObjectManager.Traps;
                Decorations = logicGameObjectManager.Decorations;
                Obstacles = logicGameObjectManager.Obstacles;
                Buildings = logicGameObjectManager.Buildings;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }

        }

        public ReplayLevel(string json)
        {
            try
            {
                var _Object = JObject.Parse(json);

                Buildings.Clear();
                foreach (var token in _Object["buildings"])
                {
                    var building =
                        JsonConvert.DeserializeObject<Building>(JsonConvert.SerializeObject(token), Settings);

                    building.Id = building.Id <= 0
                        ? Buildings.Count > 0
                            ? Buildings.Max(d => d.Id) + 1
                            : 500000000
                        : building.Id;

                    Buildings.Add(building);
                }

                Obstacles.Clear();
                foreach (var token in _Object["obstacles"])
                {
                    var obstacle =
                        JsonConvert.DeserializeObject<Obstacle>(JsonConvert.SerializeObject(token), Settings);

                    obstacle.Id = obstacle.Id <= 0
                        ? Obstacles.Count > 0
                            ? Obstacles.Max(d => d.Id) + 1
                            : 503000000
                        : obstacle.Id;

                    Obstacles.Add(obstacle);
                }

                Traps.Clear();
                foreach (var token in _Object["traps"])
                {
                    var trap = JsonConvert.DeserializeObject<Trap>(JsonConvert.SerializeObject(token), Settings);

                    trap.Id = trap.Id <= 0
                        ? Traps.Count > 0
                            ? Traps.Max(d => d.Id) + 1
                            : 504000000
                        : trap.Id;

                    Traps.Add(trap);
                }

                Decorations.Clear();
                foreach (var token in _Object["decos"])
                {
                    var deco = JsonConvert.DeserializeObject<Decoration>(JsonConvert.SerializeObject(token),
                        Settings);

                    deco.Id = deco.Id <= 0
                        ? Decorations.Count > 0
                            ? Decorations.Max(d => d.Id) + 1
                            : 506000000
                        : deco.Id;

                    Decorations.Add(deco);
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }

        }
    }
}*/