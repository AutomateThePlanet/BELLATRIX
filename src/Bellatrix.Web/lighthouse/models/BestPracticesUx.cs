using Newtonsoft.Json;
namespace Bellatrix.GoogleLighthouse
{
    public class BestPracticesUx
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}