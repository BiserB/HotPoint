using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(HotPoint.App.Areas.Identity.IdentityHostingStartup))]
namespace HotPoint.App.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}