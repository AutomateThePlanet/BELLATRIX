using Newtonsoft.Json;
namespace Bellatrix.GoogleLighthouse
{
    public class PwaOptimized
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}