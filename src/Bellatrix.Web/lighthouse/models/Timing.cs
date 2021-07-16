using System.Collections.Generic;
using Newtonsoft.Json;
namespace Bellatrix.GoogleLighthouse
{
    public class Timing
    {
        [JsonProperty("entries")]
        public List<Entry> Entries { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }
    }
}