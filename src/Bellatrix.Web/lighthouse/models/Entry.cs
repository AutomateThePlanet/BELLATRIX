using Newtonsoft.Json;
namespace Bellatrix.GoogleLighthouse
{
    public class Entry
    {
        [JsonProperty("startTime")]
        public double StartTime { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("entryType")]
        public string EntryType { get; set; }
    }
}