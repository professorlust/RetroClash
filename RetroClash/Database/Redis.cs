using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroClash.Logic;
using RetroClash.Logic.Manager;
using StackExchange.Redis;

namespace RetroClash.Database
{
    public class Redis
    {
        private static IDatabase _players;
        private static IDatabase _alliances;
        private static IServer _server;

        public static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None
        };

        public Redis()
        {
            try
            {
                var config = new ConfigurationOptions {AllowAdmin = true, ConnectTimeout = 10000, ConnectRetry = 10};

                config.EndPoints.Add(Resources.Configuration.RedisServer, 6379);

                config.Password = Resources.Configuration.RedisPassword;

                _players = ConnectionMultiplexer.Connect(config).GetDatabase(0);
                _alliances = ConnectionMultiplexer.Connect(config).GetDatabase(1);
                _server = ConnectionMultiplexer.Connect(config).GetServer(Resources.Configuration.RedisServer, 6379);
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
        }

        public static bool IsConnected => _server != null && _server.IsConnected;

        public static async Task CachePlayer(Player player)
        {
            try
            {
                await _players.StringSetAsync(player.AccountId.ToString(),
                    JsonConvert.SerializeObject(player, Settings) + "#:#:#:#" + player.LogicGameObjectManager.Json,
                    TimeSpan.FromHours(4));
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
        }

        public static async Task CacheAlliance(Alliance alliance)
        {
            try
            {
                await _alliances.StringSetAsync(alliance.Id.ToString(),
                    JsonConvert.SerializeObject(alliance, Settings), TimeSpan.FromHours(4));
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
        }

        public static async Task<Player> GetCachedPlayer(long id)
        {
            try
            {
                var data = (await _players.StringGetAsync(id.ToString())).ToString().Split("#:#:#:#".ToCharArray());

                using (var player = JsonConvert.DeserializeObject<Player>(data[0]))
                {
                    player.LogicGameObjectManager = JsonConvert.DeserializeObject<LogicGameObjectManager>(data[1]);

                    return player;
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return null;
            }
        }

        public static async Task<Alliance> GetCachedAlliance(long id)
        {
            try
            {
                return JsonConvert.DeserializeObject<Alliance>(await _alliances.StringGetAsync(id.ToString()));
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return null;
            }
        }
    }
}