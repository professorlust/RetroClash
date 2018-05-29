using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using RetroClash.Database.Models;
using RetroClash.Extensions;
using RetroClash.Logic;
using RetroClash.Logic.Manager;

namespace RetroClash.Database
{
    public class MySQL
    {
        private static string _connectionString;

        public static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.None
        };

        public MySQL()
        {
            _connectionString = new MySqlConnectionStringBuilder
            {
                Server = Resources.Configuration.MySqlServer,
                Database = Resources.Configuration.MySqlDatabase,
                UserID = Resources.Configuration.MySqlUserId,
                Password = Resources.Configuration.MySqlPassword,
                SslMode = MySqlSslMode.None,
                MinimumPoolSize = 4,
                MaximumPoolSize = 20
            }.ToString();
        }

        public static async Task ExecuteAsync(MySqlCommand cmd)
        {
            try
            {
                cmd.Connection = new MySqlConnection(_connectionString);
                await cmd.Connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
            finally
            {
                cmd.Connection?.Close();
            }
        }

        public static async Task<long> MaxPlayerId()
        {
            #region MaxPlayerId

            try
            {
                long seed;

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand("SELECT coalesce(MAX(PlayerId), 0) FROM player", connection))
                    {
                        seed = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                    }

                    await connection.CloseAsync();
                }

                return seed;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return -1;
            }

            #endregion
        }

        public static async Task<long> MaxAllianceId()
        {
            #region MaxAllianceId

            try
            {
                long seed;

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand("SELECT coalesce(MAX(ClanId), 0) FROM clan", connection))
                    {
                        seed = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                    }

                    await connection.CloseAsync();
                }

                return seed;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return -1;
            }

            #endregion
        }

        public static async Task<long> MaxReplayId()
        {
            #region MaxReplayId

            try
            {
                long seed;

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand("SELECT coalesce(MAX(Id), 0) FROM replay", connection))
                    {
                        seed = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                    }

                    await connection.CloseAsync();
                }

                return seed;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return -1;
            }

            #endregion
        }

        public static async Task<long> MaxApiId()
        {
            #region MaxApiId

            try
            {
                long seed;

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand("SELECT coalesce(MAX(Id), 0) FROM api", connection))
                    {
                        seed = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                    }

                    await connection.CloseAsync();
                }

                return seed;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return -1;
            }

            #endregion
        }

        public static async Task<long> PlayerCount()
        {
            #region PlayerCount

            try
            {
                long seed;

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM player", connection))
                    {
                        seed = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                    }

                    await connection.CloseAsync();
                }

                return seed;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return 0;
            }

            #endregion
        }

        public static async Task<long> AllianceCount()
        {
            #region AllianceCount

            try
            {
                long seed;

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM clan", connection))
                    {
                        seed = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                    }

                    await connection.CloseAsync();
                }

                return seed;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return 0;
            }

            #endregion
        }

        public static async Task<Player> CreatePlayer()
        {
            try
            {
                var id = await MaxPlayerId();
                if (id <= -1)
                    return null;

                var player = new Player(id + 1, Utils.GenerateToken);

                using (var cmd =
                    new MySqlCommand(
                        $"INSERT INTO `player`(`PlayerId`, `Score`, `Language`, `Avatar`, `GameObjects`) VALUES ({player.AccountId}, {player.Score}, @language, @avatar, @objects)")
                )
                {
#pragma warning disable 618
                    cmd.Parameters?.Add("@language", player.Language);
                    cmd.Parameters?.Add("@avatar", JsonConvert.SerializeObject(player, Settings));
                    cmd.Parameters?.Add("@objects", player.LogicGameObjectManager.Json);
#pragma warning restore 618

                    await ExecuteAsync(cmd);
                }

                return player;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return null;
            }
        }

        public static async Task<Alliance> CreateAlliance()
        {
            try
            {
                var id = await MaxAllianceId();
                if (id <= -1)
                    return null;

                var alliance = new Alliance(id + 1);

                using (var cmd = new MySqlCommand(
                    $"INSERT INTO `clan`(`ClanId`, `Name`, `Score`, `IsFull`, `Data`) VALUES ({alliance.Id}, @name, {alliance.Score}, false, @data)")
                )
                {
#pragma warning disable 618
                    cmd.Parameters?.Add("@name", alliance.Name);
                    cmd.Parameters?.Add("@data", JsonConvert.SerializeObject(alliance, Settings));
#pragma warning restore 618

                    await ExecuteAsync(cmd);
                }

                return alliance;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return null;
            }
        }

        public static async Task CreateApiInfo()
        {
            try
            {
                var id = await MaxApiId();
                if (id <= -1)
                    return;

                var info = new ApiInfo
                {
                    Id = id + 1,
                    Online = Resources.PlayerCache.Count,
                    PlayersInDb = await PlayerCount(),
                    AlliancesInDb = await AllianceCount(),
                    Status = Configuration.Maintenance ? "Maintenance" : "Online",
                    CurrentDateTime = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)
                };

                using (var cmd =
                    new MySqlCommand($"INSERT INTO `api`(`Id`, `Info`) VALUES ({info.Id}, @info)"))
                {
#pragma warning disable 618
                    cmd.Parameters?.Add("@info", JsonConvert.SerializeObject(info));
#pragma warning restore 618

                    await ExecuteAsync(cmd);
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
        }

        public static async Task<List<Player>> GetGlobalPlayerRanking()
        {
            var list = new List<Player>();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand("SELECT * FROM `player` ORDER BY `Score` DESC LIMIT 200",
                        connection))
                    {
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            var player = JsonConvert.DeserializeObject<Player>((string) reader["Avatar"], Settings);
                            player.LogicGameObjectManager =
                                JsonConvert.DeserializeObject<LogicGameObjectManager>((string) reader["GameObjects"],
                                    Settings);

                            list.Add(player);
                        }
                    }
                    await connection.CloseAsync();
                }

                return list;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return list;
            }
        }

        public static async Task<List<Alliance>> GetGlobalAllianceRanking()
        {
            var list = new List<Alliance>();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand("SELECT * FROM `clan` ORDER BY `Score` DESC LIMIT 200",
                        connection))
                    {
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            var alliance = JsonConvert.DeserializeObject<Alliance>((string) reader["Data"], Settings);

                            list.Add(alliance);
                        }
                    }

                    await connection.CloseAsync();
                }

                return list;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return list;
            }
        }

        public static async Task<List<Player>> GetLocalPlayerRanking(string language)
        {
            var list = new List<Player>();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd =
                        new MySqlCommand(
                            $"SELECT * FROM `player` WHERE Language = '{language}' ORDER BY `Score` DESC LIMIT 200",
                            connection))
                    {
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            var player = JsonConvert.DeserializeObject<Player>((string) reader["Avatar"], Settings);
                            player.LogicGameObjectManager =
                                JsonConvert.DeserializeObject<LogicGameObjectManager>((string) reader["GameObjects"],
                                    Settings);

                            list.Add(player);
                        }
                    }

                    await connection.CloseAsync();
                }

                return list;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return list;
            }
        }

        public static async Task<List<Alliance>> GetJoinableAlliances(int limit)
        {
            var list = new List<Alliance>();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand($"SELECT * FROM `clan` WHERE IsFull = '0' LIMIT {limit}",
                        connection))
                    {
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            var alliance = JsonConvert.DeserializeObject<Alliance>((string) reader["Data"], Settings);

                            list.Add(alliance);
                        }
                    }

                    await connection.CloseAsync();
                }

                return list;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return list;
            }
        }

        public static async Task<Player> GetPlayer(long id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    Player player = null;

                    using (var cmd = new MySqlCommand($"SELECT * FROM `player` WHERE PlayerId = '{id}'", connection))
                    {
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            player = JsonConvert.DeserializeObject<Player>((string) reader["Avatar"], Settings);
                            player.LogicGameObjectManager =
                                JsonConvert.DeserializeObject<LogicGameObjectManager>((string) reader["GameObjects"],
                                    Settings);
                        }
                    }

                    await connection.CloseAsync();

                    return player;
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return null;
            }
        }

        public static async Task<Player> GetPlayerByFacebookId(string facebookId)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    Player player = null;

                    using (var cmd = new MySqlCommand($"SELECT * FROM `player` WHERE FacebookId = '{facebookId}'", connection))
                    {
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            player = JsonConvert.DeserializeObject<Player>((string)reader["Avatar"], Settings);
                            player.LogicGameObjectManager =
                                JsonConvert.DeserializeObject<LogicGameObjectManager>((string)reader["GameObjects"],
                                    Settings);
                        }
                    }

                    await connection.CloseAsync();

                    return player;
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return null;
            }
        }

        public static async Task<Alliance> GetAlliance(long id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    Alliance alliance = null;

                    using (var cmd = new MySqlCommand($"SELECT * FROM `clan` WHERE ClanId = '{id}'", connection))
                    {
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                            alliance = JsonConvert.DeserializeObject<Alliance>((string) reader["Data"], Settings);
                    }

                    await connection.CloseAsync();

                    return alliance;
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return null;
            }
        }

        public static async Task<string> GetReplay(long id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string data = null;

                    using (var cmd = new MySqlCommand($"SELECT * FROM `replay` WHERE Id = '{id}'", connection))
                    {
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                            return reader["Data"].ToString();
                    }

                    await connection.CloseAsync();

                    return data;
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return null;
            }
        }

        public static async Task<long> SaveReplay(Battle battle)
        {
            try
            {
                var id = await MaxReplayId();
                if (id <= -1)
                    return -1;

                using (var cmd = new MySqlCommand(
                    $"INSERT INTO `replay`(`Id`, `Data`) VALUES ({id + 1}, @data)")
                )
                {
#pragma warning disable 618
                    cmd.Parameters?.Add("@data", battle.GetReplayJson);
#pragma warning restore 618

                    await ExecuteAsync(cmd);

                    return id + 1;
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
                return -1;
            }
        }

        public static async Task SavePlayer(Player player)
        {
            try
            {
                using (var cmd =
                    new MySqlCommand(
                        $"UPDATE `player` SET `Score`='{player.Score}', `Language`='{player.Language}', `FacebookId`=@fb, `Avatar`=@avatar, `GameObjects`=@objects WHERE PlayerId = '{player.AccountId}'")
                )
                {
#pragma warning disable 618
                    cmd.Parameters?.Add("@fb", player.FacebookId);
                    cmd.Parameters?.Add("@avatar", JsonConvert.SerializeObject(player, Settings));
                    cmd.Parameters?.Add("@objects", player.LogicGameObjectManager.Json);
#pragma warning restore 618

                    await ExecuteAsync(cmd);
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
        }

        public static async Task SaveAlliance(Alliance alliance)
        {
            try
            {
                using (var cmd = new MySqlCommand(
                    $"UPDATE `clan` SET `Name`=@name, `Score`='{alliance.Score}', `IsFull`=@isFull, `Data`=@data WHERE ClanId = '{alliance.Id}'")
                )
                {
#pragma warning disable 618
                    cmd.Parameters?.Add("@name", alliance.Name);
                    cmd.Parameters?.Add("@isFull", alliance.IsFull);
                    cmd.Parameters?.Add("@data", JsonConvert.SerializeObject(alliance, Settings));
#pragma warning restore 618

                    await ExecuteAsync(cmd);
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
        }

        public static async Task DeletePlayer(long id)
        {
            try
            {
                using (var cmd = new MySqlCommand(
                    $"DELETE FROM `player` WHERE PlayerId = '{id}'")
                )
                {
                    await ExecuteAsync(cmd);
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
        }
    }
}