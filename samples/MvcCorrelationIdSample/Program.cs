using System.IO;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MvcCorrelationIdSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new HostBuilder()
                       .UseContentRoot(Directory.GetCurrentDirectory())
                       .ConfigureWebHostDefaults(webBuilder =>
                                                 {
                                                     webBuilder.UseStartup<Startup>();
                                                 })
                       .Build();

            host.Run();
        }
    }
}
