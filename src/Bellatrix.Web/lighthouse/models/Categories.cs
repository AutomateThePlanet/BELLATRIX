using Newtonsoft.Json;
namespace Bellatrix.GoogleLighthouse
{
    public class Categories
    {
        [JsonProperty("performance")]
        public Performance Performance { get; set; }
    }
}