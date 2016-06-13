using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Marvin.Core.Models
{
    public class ActionMessage
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("payload")]
        public Dictionary<string, object> Payload { get; set; }
    }
}
