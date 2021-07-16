using Newtonsoft.Json;
namespace Bellatrix.GoogleLighthouse
{
    public class LighthouseCoreAuditsThirdPartySummaryJsDisplayValue
    {
        [JsonProperty("values")]
        public Values Values { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
}