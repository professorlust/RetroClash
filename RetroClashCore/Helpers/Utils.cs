using System;

namespace RetroClashCore.Helpers
{
    public class Utils
    {
        public static string GenerateToken
        {
            get
            {
                var random = new Random();
                var token = string.Empty;

                for (var i = 0; i < 40; i++)
                    token += "abcdefghijklmnopqrstuvwxyz0123456789"[random.Next(36)];

                return token;
            }
        }

        public static string GenerateRc4Key
        {
            get
            {
                var random = new Random();
                var token = string.Empty;

                for (var i = 0; i < 38; i++)
                    token += "abcdefghijklmnopqrstuvwxyz0123456789"[random.Next(36)];

                return token;
            }
        }

        public static int GetSecondsUntilNextMonth
        {
            get
            {
                var now = DateTime.UtcNow;

                if (now.Month != 12)
                    return (int) (new DateTime(now.Year, now.Month + 1, 1, now.Hour,
                                      now.Minute, now.Second) - now).TotalSeconds;

                return (int) (new DateTime(now.Year, 1, 1, now.Hour,
                                  now.Minute, now.Second) - now).TotalSeconds;
            }
        }

        public static int GetCurrentTimestamp => (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

        public static int ToTick(TimeSpan duration) => (int)(duration.TotalMilliseconds / 16.6666666666667);

        public static int ToTick(int seconds) => (int)(seconds * 1000 / 16.6666666666667);

        public static double FromTick(int tick) => tick * 16.6666666666667 / 1000d;

        public static int[] GetHighLow(long id) => new[] {Convert.ToInt32(id >> 32), (int)id };
    }
}