using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using RetroClash.Logic;

namespace RetroClash.Database.Caching
{
    public class AllianceCache : Dictionary<long, Alliance>
    {
        private readonly object _gate = new object();

        public Timer Timer = new Timer(10000)
        {
            AutoReset = true
        };

        public AllianceCache()
        {
            Timer.Elapsed += TimerCallback;
            Timer.Start();
        }

        public void AddAlliance(Alliance alliance)
        {
            lock (_gate)
            {
                try
                {
                    if (ContainsKey(alliance.Id))
                        return;

                    alliance.Timer.Elapsed += alliance.SaveCallback;
                    alliance.Timer.Start();

                    Add(alliance.Id, alliance);
                }
                catch (Exception exception)
                {
                    Logger.Log(exception, Enums.LogType.Error);
                }
            }
        }

        public async Task<Alliance> GetAlliance(long id)
        {
            lock (_gate)
            {
                if (ContainsKey(id))
                    return this[id];
            }

            var alliance = await MySQL.GetAlliance(id);
            AddAlliance(alliance);
            return alliance;
        }

        public void RemoveAlliance(long id)
        {
            lock (_gate)
            {
                try
                {
                    if (!ContainsKey(id)) return;

                    var alliance = this[id];

                    alliance.Timer.Stop();
                    MySQL.SaveAlliance(alliance).Wait();

                    Remove(id);
                }
                catch (Exception exception)
                {
                    Logger.Log(exception, Enums.LogType.Error);
                }
            }
        }

        private void TimerCallback(object state, ElapsedEventArgs args)
        {
            foreach (var alliance in Values)
                if (alliance.Members.Sum(x => x.IsOnline ? 1 : 0) <= 0)
                    RemoveAlliance(alliance.Id);
        }
    }
}