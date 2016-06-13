using System.Threading.Tasks;

namespace Marvin.Core.Http
{
    public interface IMarvinEndpointGetHandler : IMarvinEndpointHandler
    {
        Task<string> Handle();
    }
}
