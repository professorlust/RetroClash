using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Database.Caching
{
    public class PlayerCache : ConcurrentDictionary<long, Player>
    {
        public Player Random
        {
            get
            {
               // if (Count <= 1) return null;
                return this.ElementAt(new Random().Next(0, Count - 1)).Value;
            }
        }

        public async Task<bool> AddPlayer(long id, Player player)
        {
            try
            {
                if (ContainsKey(id))
                {
                    await Resources.Gateway.Send(new DisconnectedMessage(this[id].Device));
                    await RemovePlayer(id, this[id].Device.SessionId);
                }

                player.Timer.Elapsed += player.SaveCallback;
                player.Timer.Start();

                return TryAdd(id, player);
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
                return false;
            }
        }

        public async Task<Player> GetPlayer(long id, bool onlineOnly = false)
        {
            if (ContainsKey(id))
            {
                TryGetValue(id, out var value);
                return value;
            }

            if (onlineOnly) return null;
            if (!Redis.IsConnected) return await MySQL.GetPlayer(id);
            var player = await Redis.GetCachedPlayer(id);

            if (player != null)
                return player;

            return await MySQL.GetPlayer(id);
        }

        public async Task<bool> RemovePlayer(long id, Guid sessionId)
        {
            try
            {
                if (!ContainsKey(id)) return true;

                var player = this[id];

                player.Timer.Stop();
                await MySQL.SavePlayer(player);

                return player.Device.SessionId == sessionId && TryRemove(id, out var value);
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
                return false;
            }
        }
    }
}