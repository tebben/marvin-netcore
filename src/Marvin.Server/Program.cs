using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Marvin.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
             //   .UseUrls("http://0.0.0.0:80")
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
