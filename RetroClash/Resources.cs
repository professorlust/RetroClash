using System;
using RetroClash.Database;
using RetroClash.Database.Caching;
using RetroClash.Files;
using RetroClash.Logic.Manager;
using RetroClash.Network;
using RetroClash.Protocol;

namespace RetroClash
{
    public class Resources : IDisposable
    {
        private static LogicMagicMessageFactory _messagefactory;
        private static LogicCommandManager _commandfactory;
        private static DebugCommandFactory _debugcommandfactory;
        private static MySQL _mysql;
        private static ApiService _apiService;
        private static Logger _logger;

        public Resources()
        {
            Configuration = new Configuration();
            Configuration.Initialize();

            _logger = new Logger();

            Csv = new Csv();
            Fingerprint = new Fingerprint();

            _mysql = new MySQL();
            _messagefactory = new LogicMagicMessageFactory();
            _commandfactory = new LogicCommandManager();
            _debugcommandfactory = new DebugCommandFactory();
            _apiService = new ApiService();

            Levels = new Levels();
            PlayerCache = new PlayerCache();
            AllianceCache = new AllianceCache();
            LeaderboardCache = new LeaderboardCache();

            ChatManager = new LogicGlobalChatManager();

            Gateway = new Gateway();
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
            _apiService = null;
            _logger = null;
        }
    }
}