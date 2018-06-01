using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroClashCore.Helpers;

namespace RetroClashCore.Logic
{
    public class LogicLong : ICloneable
    {
        [JsonProperty("high")]
        public readonly int High;

        [JsonProperty("low")]
        public readonly int Low;

        public LogicLong(int high, int low)
        {
            High = high;
            Low = low;
        }

        public LogicLong(long value)
        {
            High = Convert.ToInt32(value >> 32);
            Low = (int)value;
        }

        public LogicLong()
        {
            
        }

        public async Task Encode(MemoryStream stream)
        {
            await stream.WriteIntAsync(High);
            await stream.WriteIntAsync(Low);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ High;
                result = (result * 397) ^ Low;
                return result;
            }
        }

        public override string ToString()
        {
            return $"LogicLong({High}, {Low})";
        }

        public bool IsZero()
        {
            return High == 0 && Low == 0;
        }
    }
}
