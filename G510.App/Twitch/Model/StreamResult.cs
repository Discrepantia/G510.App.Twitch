using System.Collections.Generic;
using Newtonsoft.Json;

namespace Twitch.Net.Model
{
    public class StreamResult
    {
        [JsonProperty("_links")]
        public Dictionary<string, object> Links { get; set; }
        [JsonProperty("stream")]
        public Stream Stream { get; set; }
    }
}
