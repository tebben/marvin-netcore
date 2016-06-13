using System;
using System.Collections.Generic;
using Marvin.Core.Http;
using Marvin.Core.Module;
using Marvin.Core.System;
using Marvin.Core.System.HttpHandlers;

namespace Marvin.Core
{
    public delegate void MarvinInitialised(object sender);

    public class MarvinSystem
    {
        public ActionSystem ActionSystem { get; private set; }
        public List<IModule> Modules { get; private set; }
        public List<MarvinEndpoint> Endpoints { get; private set; }

        public event MarvinInitialised Initialised;

        /// <summary>
        /// Start Marvin.Core, Start will load available modules and triggers
        /// MarvinInitialised when ready
        /// </summary>
        public void Start()
        {
            Console.Out.WriteLine("Marvin system starting");

            Modules = ModuleProvider.LoadModules();
            WireActions(Modules);
            Endpoints = CreateEndpoints();

            Console.Out.WriteLine("Marvin system ready for lift off");
            Initialised?.Invoke(this);
        }

        private void WireActions(IEnumerable<IModule> modules)
        {
            ActionSystem = new ActionSystem();

            foreach (var module in modules)
            {
                if(module.GetActions() == null)
                    continue;

                ActionSystem.Add(module.GetActions());
            }
        }

        private List<MarvinEndpoint> CreateEndpoints()
        {
            var endpoints = new List<MarvinEndpoint>
            {
                new MarvinEndpoint("modules", "Retrieve all registered modules", new GetModuleHandler(Modules))
            };

            return endpoints;
        }
    }
}
