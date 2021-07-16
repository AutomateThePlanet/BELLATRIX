using Newtonsoft.Json;
namespace Bellatrix.GoogleLighthouse
{
    public class PwaInstallable
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}