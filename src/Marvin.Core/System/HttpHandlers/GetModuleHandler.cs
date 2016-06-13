using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Marvin.Core.Http;
using Marvin.Core.Module;
using Newtonsoft.Json;

namespace Marvin.Core.System.HttpHandlers
{
    public class GetModuleHandler : IMarvinEndpointGetHandler
    {
        private List<IModule> Modules { get; }

        public GetModuleHandler(List<IModule> modules)
        {
            Modules = modules;
        }

        public async Task<string> Handle()
        {
            var json = JsonConvert.SerializeObject(Modules);
            return await Task.Run(() => json);
        }
    }
}
