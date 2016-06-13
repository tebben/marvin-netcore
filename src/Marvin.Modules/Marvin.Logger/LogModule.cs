using System.Collections.Generic;
using System.Composition;
using Marvin.Core.Http;
using Marvin.Core.Module;
using Marvin.Logger.Actions;
using Marvin.Logger.Handlers;
using Marvin.Logger.Models;

namespace Marvin.Logger
{
    [Export(typeof(IModule))]
    public class LogModule : MarvinModule
    {
        public LogModule() : base("Marvin Logger", "Write and read log messages")
        {
            Actions = new List<IAction> {new WriteAction()};

            Endpoints = new List<MarvinEndpoint> {new MarvinEndpoint("log", "retrieve the logfile", new GetLogHandler())};
        }
    }
}
