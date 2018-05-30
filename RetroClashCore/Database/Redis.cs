using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroClashCore.Logic;
using RetroClashCore.Logic.Manager;
using StackExchange.Redis;

namespace RetroClashCore.Database
{
    public class Redis
    {
        private static IDatabase _players;
        private static IDatabase _alliances;
        private static IServer _server;

        private static ConnectionMultiplexer _connection;

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

                _connection = ConnectionMultiplexer.Connect(config);

                _players = _connection.GetDatabase(0);
                _alliances = _connection.GetDatabase(1);
                _server = _connection.GetServer(Resources.Configuration.RedisServer, 6379);
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
        }

        public static bool IsConnected => _server != null;

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

        public static int CachedPlayers() => Convert.ToInt32(
            _connection.GetServer(Resources.Configuration.RedisServer, 6379).Info("keyspace")[0]
                .ElementAt(0)
                .Value
                .Split(new[] { "keys=" }, StringSplitOptions.None)[1]
                .Split(new[] { ",expires=" }, StringSplitOptions.None)[0]);
    }
}