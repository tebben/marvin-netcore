using System.Collections.Generic;
using Marvin.Core.Http;

namespace Marvin.Core.Module
{
    public interface IModule
    {
        string GetName();
        string GetDescription();

        List<IEvent> GetEvents();
        List<IAction> GetActions();
        List<MarvinEndpoint> GetEndpoints();
    }
}
