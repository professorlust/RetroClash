using System;
using System.IO;
using Newtonsoft.Json;

namespace RetroClashCore
{
    public class Configuration
    {
        [JsonIgnore] public const int MaxClients = 100;

        [JsonIgnore] public const int OpsToPreAlloc = 2;

        [JsonIgnore] public const int BufferSize = 1024;

        [JsonIgnore] public static bool Debug = false;

        [JsonIgnore] public static bool Maintenance = false;

        [JsonProperty("encryption_key")] public string EncryptionKey = "fhsd6f86f67rt8fw78fw789we78r9789wer6re";

        [JsonProperty("mysql_database")] public string MySqlDatabase = "rcdb";

        [JsonProperty("mysql_password")] public string MySqlPassword = "";

        [JsonProperty("mysql_server")] public string MySqlServer = "127.0.0.1";

        [JsonProperty("mysql_user")] public string MySqlUserId = "root";

        [JsonProperty("patch_url")] public string PatchUrl = "";

        [JsonProperty("redis_password")] public string RedisPassword = "";

        [JsonProperty("redis_server")] public string RedisServer = "127.0.0.1";

        [JsonProperty("server_port")] public int ServerPort = 9339;

        [JsonProperty("api_port")] public int ApiPort = 4800;

        [JsonProperty("update_url")] public string UpdateUrl = "https://retroclash.pw/";

        public void Initialize()
        {
            if (File.Exists("config.json"))
                try
                {
                    var config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText("config.json"));

                    UpdateUrl = config.UpdateUrl;
                    PatchUrl = config.PatchUrl;
                    MySqlUserId = config.MySqlUserId;
                    MySqlServer = config.MySqlServer;
                    MySqlPassword = config.MySqlPassword;
                    MySqlDatabase = config.MySqlDatabase;
                    RedisPassword = config.RedisPassword;
                    RedisServer = config.RedisServer;
                    EncryptionKey = config.EncryptionKey;
                    ServerPort = config.ServerPort;
                    ApiPort = config.ApiPort;
                }
                catch (Exception)
                {
                    Console.WriteLine("Couldn't load configuration.");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            else
                try
                {
                    Console.WriteLine("Welcome to RetroClash! Let's setup the server in just 8 steps.");

                    Console.Write("STEP 1: Enter the update url for outdated clients: ");
                    var updateUrl = Console.ReadLine();

                    if (!string.IsNullOrEmpty(updateUrl))
                        UpdateUrl = updateUrl;

                    Console.Write("STEP 2: Enter the content url for asset updates: ");
                    var contentUrl = Console.ReadLine();

                    if (!string.IsNullOrEmpty(contentUrl))
                        PatchUrl = contentUrl;

                    Console.Write("STEP 3: Enter the mysql username: ");
                    var mysqlUser = Console.ReadLine();

                    if (!string.IsNullOrEmpty(mysqlUser))
                        MySqlUserId = mysqlUser;

                    Console.Write("STEP 4: Enter the mysql password: ");
                    var mysqlPassword = Console.ReadLine();

                    if (!string.IsNullOrEmpty(mysqlPassword))
                        MySqlPassword = mysqlPassword;

                    Console.Write("STEP 5: Enter the mysql server host: ");
                    var mysqlServer = Console.ReadLine();

                    if (!string.IsNullOrEmpty(mysqlServer))
                        MySqlServer = mysqlServer;

                    Console.Write("STEP 6: Enter the name of the mysql database: ");
                    var mysqlDatabase = Console.ReadLine();

                    if (!string.IsNullOrEmpty(mysqlDatabase))
                        MySqlDatabase = mysqlDatabase;

                    Console.Write("STEP 7: Enter the redis host: ");
                    var redisHost = Console.ReadLine();

                    if (!string.IsNullOrEmpty(redisHost))
                        RedisServer = redisHost;

                    Console.Write("STEP 8: Enter the redis password: ");
                    var redisPassword = Console.ReadLine();

                    if (!string.IsNullOrEmpty(redisPassword))
                        RedisPassword = redisPassword;

                    File.WriteAllText("config.json", JsonConvert.SerializeObject(this, Formatting.Indented));

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Setup completed. Restart the server now.");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Couldn't create config file.");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
        }
    }
}