using Newtonsoft.Json;
namespace Bellatrix.GoogleLighthouse
{
    public class Summary
    {
        [JsonProperty("wastedMs")]
        public double WastedMs { get; set; }

        [JsonProperty("wastedBytes")]
        public double WastedBytes { get; set; }
    }
}