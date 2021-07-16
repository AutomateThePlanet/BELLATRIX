using Newtonsoft.Json;
namespace Bellatrix.GoogleLighthouse
{
    public class LongestChain
    {
        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("length")]
        public double Length { get; set; }

        [JsonProperty("transferSize")]
        public double TransferSize { get; set; }
    }
}