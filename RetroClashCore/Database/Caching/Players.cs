using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using RetroClashCore.Logic;
using RetroClashCore.Protocol.Messages.Server;
using RetroGames.Logic;

namespace RetroClashCore.Database.Caching
{
    public class Players : ConcurrentDictionary<long, Player>
    {
        private readonly Timer _slaveTimer = new Timer(10000)
        {
            AutoReset = true
        };

        public Players()
        {
            _slaveTimer.Elapsed += SlaveTimerOnElapsed;
            _slaveTimer.Start();
        }

        private async void SlaveTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            await Task.Run(async () =>
            {
                foreach (var player in Values)
                    if (player.Device.TimeSinceLastKeepAlive > 40)
                    {
                        player.Device.Disconnect();
                        await RemovePlayer(player.AccountId, player.Device.SessionId);
                    }
            });
        }

        public async Task<Player> Random()
        {
            if (Count > 10)
                return this.ElementAt(new Random().Next(0, Count - 1)).Value;

            return await Redis.GetRandomCachedPlayer();
        }

        public async Task<bool> AddPlayer(long id, Player player)
        {
            try
            {
                if (id > 0 && ContainsKey(id))
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

            if (!Redis.IsConnected) return await PlayerDb.Get(id);

            var player = await Redis.GetCachedPlayer(id);

            if (player != null)
                return player;

            player = await PlayerDb.Get(id);

            await Redis.CachePlayer(player);

            return player;
        }

        public async Task<Player> GetPlayer(LogicLong logicLong)
        {
            return await GetPlayer(logicLong.Long);
        }

        public async Task<bool> RemovePlayer(long id, Guid sessionId)
        {
            try
            {
                if (!ContainsKey(id)) return true;

                var player = this[id];

                player.Timer.Stop();

                if (Redis.IsConnected)
                    await Redis.CachePlayer(player);

                await PlayerDb.Save(player);

                return player.Device.SessionId == sessionId && TryRemove(id, out var _);
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
                return false;
            }
        }
    }
}