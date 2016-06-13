using System.Threading.Tasks;
using Marvin.Core.Http;

namespace Marvin.Logger.Handlers
{
    public class GetLogHandler : IMarvinEndpointGetHandler
    {
        public async Task<string> Handle()
        {
            return await Task.Run(() => "HALLO IK BEN DE LOGGER!!!");
        }
    }
}
