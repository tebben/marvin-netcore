using Newtonsoft.Json;

namespace Marvin.Logger.Models
{
    public class MessagePayload
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
