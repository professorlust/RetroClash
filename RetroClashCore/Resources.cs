using System;
using RetroClashCore.Database;
using RetroClashCore.Database.Caching;
using RetroClashCore.Files;
using RetroClashCore.Helpers;
using RetroClashCore.Logic.Manager;
using RetroClashCore.Network;
using RetroClashCore.Protocol;

namespace RetroClashCore
{
    public class Resources : IDisposable
    {
        private static LogicMagicMessageFactory _messagefactory;
        private static LogicCommandManager _commandfactory;
        private static DebugCommandFactory _debugcommandfactory;
        private static MySQL _mysql;
        private static Logger _logger;
        private static Redis _redis;

        public Resources()
        {
            Configuration = new Configuration();
            Configuration.Initialize();

            _logger = new Logger();

            Logger.Log($"OS: {(Utils.IsLinux ? "Linux" : "Windows")}");

            Csv = new Csv();
            Fingerprint = new Fingerprint();

            _mysql = new MySQL();

            if(!string.IsNullOrEmpty(Configuration.RedisPassword) && !string.IsNullOrEmpty(Configuration.RedisServer))
                _redis = new Redis();

            _messagefactory = new LogicMagicMessageFactory();
            _commandfactory = new LogicCommandManager();
            _debugcommandfactory = new DebugCommandFactory();

            Levels = new Levels();
            PlayerCache = new PlayerCache();
            AllianceCache = new AllianceCache();
            LeaderboardCache = new LeaderboardCache();

            ChatManager = new LogicGlobalChatManager();

            Gateway = new Gateway();
            Gateway.StartAsync().Wait();
        }

        public static Gateway Gateway { get; set; }
        public static PlayerCache PlayerCache { get; set; }
        public static AllianceCache AllianceCache { get; set; }
        public static LeaderboardCache LeaderboardCache { get; set; }
        public static Configuration Configuration { get; set; }
        public static Levels Levels { get; set; }
        public static LogicGlobalChatManager ChatManager { get; set; }
        public static Fingerprint Fingerprint { get; set; }
        public static Csv Csv { get; set; }

        public void Dispose()
        {
            Csv = null;
            Gateway = null;
            PlayerCache = null;
            Configuration = null;
            Levels = null;
            Fingerprint = null;
            _messagefactory = null;
            _commandfactory = null;
            _debugcommandfactory = null;
            _mysql = null;
            _logger = null;
        }
    }
}