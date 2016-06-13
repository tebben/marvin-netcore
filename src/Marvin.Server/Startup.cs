using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Marvin.Core;
using Marvin.Core.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Marvin.Server
{
    public class Startup
    {
        private MarvinSystem MarvinSystem { get; set; }
        private Websockethandler Websockethandler { get; set; }

        public delegate Task MarvinHttpOperation(HttpContext c, IMarvinEndpointHandler innerHandler);

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            MarvinSystem = new MarvinSystem();
            MarvinSystem.Start();

            var builder = new RouteBuilder(app);
            MarvinHttpOperation handler = MarvinHttpHandler;
            foreach (var marvinEndpoint in MarvinSystem.Endpoints)
            {
                builder.MapRoute(marvinEndpoint.Path, c => handler(c, marvinEndpoint.Handler));
            }

            foreach (var module in MarvinSystem.Modules)
            {
                foreach (var marvinEndpoint in module.GetEndpoints())
                {
                    builder.MapRoute(marvinEndpoint.Path, c => handler(c, marvinEndpoint.Handler));
                }
            }

            app.UseRouter(builder.Build());
            app.UseStaticFiles();
            app.UseWebSockets();
            Websockethandler = new Websockethandler(app, MarvinSystem.ActionSystem);
        }

        // ToDO use OWIN?
        private static async Task MarvinHttpHandler(HttpContext c, IMarvinEndpointHandler innerHandler)
        {
            if (innerHandler is IMarvinEndpointGetHandler)
            {
                await c.Response.WriteAsync(await ((IMarvinEndpointGetHandler)innerHandler).Handle());
            }
            
        }
    }
}
