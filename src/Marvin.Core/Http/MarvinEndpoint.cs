using Newtonsoft.Json;

namespace Marvin.Core.Http
{
    public class MarvinEndpoint
    {
        [JsonProperty("path")]
        public string Path { get; private set; }

        [JsonProperty("description")]
        public string Description { get; private set; }

        [JsonIgnore]
        public IMarvinEndpointHandler Handler { get; private set; }

        public MarvinEndpoint(string path, string description, IMarvinEndpointHandler handler)
        {
            Path = path;
            Description = description;
            Handler = handler;
        }
    }
}
