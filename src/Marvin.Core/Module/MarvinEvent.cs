namespace Marvin.Core.Module
{
    public abstract class MarvinEvent : IEvent
    {
        public string Name { get; }
        public string Event { get; }
        public string Description { get; }

        public MarvinEvent(string name, string description, string eventName)
        {
            Name = name;
            Description = description;
            Event = eventName;
        }

        /// <summary>
        /// Retrieve the event name, used for displaying only
        /// </summary>
        public string GetName() => Name;

        /// <summary>
        /// Retrieve the event
        /// </summary>
        public string GetEvent() => Event;

        /// <summary>
        /// Retrieve the event description, used for displaying only
        /// </summary>
        public string GetDescription() => Description;
    }
}
