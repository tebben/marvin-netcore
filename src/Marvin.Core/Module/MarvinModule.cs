using System.Collections.Generic;
using Marvin.Core.Http;
using Newtonsoft.Json;

namespace Marvin.Core.Module
{
    public abstract class MarvinModule : IModule
    {
        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("description")]
        public string Description { get; }

        [JsonProperty("events")]
        public List<IEvent> Events { get; set; }

        [JsonProperty("actions")]
        public List<IAction> Actions { get; set; }

        [JsonProperty("endpoints")]
        public List<MarvinEndpoint> Endpoints { get; set; }

        /// <summary>
        /// Create a new module
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public MarvinModule(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Retrieve the module name
        /// </summary>
        public string GetName() => Name;

        /// <summary>
        /// Retrieve the module description
        /// </summary>
        public string GetDescription() => Description;

        /// <summary>
        /// Retreive all the endpoints defined for the module
        /// </summary>
        public List<MarvinEndpoint> GetEndpoints() => Endpoints;

        /// <summary>
        /// Retreive all actions defined for the module
        /// </summary>
        public List<IAction> GetActions() => Actions;

        /// <summary>
        /// Retreive all events defined for the module
        /// </summary>
        public List<IEvent> GetEvents() => Events;
    }
}
