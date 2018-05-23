using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Database.Caching
{
    public class PlayerCache : Dictionary<long, Player>
    {
        private readonly object _gate = new object();

        public Player Random
        {
            get
            {
                lock (_gate)
                {
                    if (Count <= 1) return null;
                    return this.ElementAt(new Random().Next(0, Count - 1)).Value;
                }
            }
        }

        public void AddPlayer(Player player)
        {
            lock (_gate)
            {
                try
                {
                    if (ContainsKey(player.AccountId))
                    {
                        RemovePlayer(player.AccountId);
                    }

                    player.Timer.Elapsed += player.SaveCallback;
                    player.Timer.Start();

                    Add(player.AccountId, player);
                }
                catch (Exception exception)
                {
                    Logger.Log(exception, Enums.LogType.Error);
                }
            }
        }

        public async Task<Player> GetPlayer(long id, bool onlineOnly = false)
        {
            lock (_gate)
            {
                if (ContainsKey(id))
                    return this[id];
            }

            if (!onlineOnly)
                return await MySQL.GetPlayer(id);
            return null;
        }

        public void RemovePlayer(long id)
        {
            lock (_gate)
            {
                try
                {
                    if (!ContainsKey(id)) return;

                    var player = this[id];

                    player.Timer.Stop();
                    MySQL.SavePlayer(player).Wait();

                    Remove(id);
                }
                catch (Exception exception)
                {
                    Logger.Log(exception, Enums.LogType.Error);
                }
            }
        }
    }
}