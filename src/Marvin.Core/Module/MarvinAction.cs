using System.Collections.Generic;
using Marvin.Core.Models;
using Newtonsoft.Json;

namespace Marvin.Core.Module
{
    public abstract class MarvinAction : IAction
    {
        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("action")]
        public string Action { get; }

        [JsonProperty("description")]
        public string Description { get; }

        [JsonProperty("sample")]
        public ActionMessage Sample { get; set; }

        public MarvinAction(string name, string description, string action)
        {
            Name = name;
            Description = description;
            Action = action;
        }

        /// <summary>
        /// Retrieve the action name, used for displaying only
        /// </summary>
        public string GetName() => Name;

        /// <summary>
        /// Retrieve the action, based on this name the Execute method gets triggered, needs to be unique
        /// </summary>
        public string GetAction() => Action;

        /// <summary>
        /// Retrieve the action description, used for displaying only
        /// </summary>
        public string GetDescription() => Description;

        /// <summary>
        /// Retrieve the sample action
        /// </summary>
        public ActionMessage GetSample() => Sample;

        /// <summary>
        /// Execute the action
        /// </summary>
        public abstract void Execute(ActionMessage msg);
    }
}
