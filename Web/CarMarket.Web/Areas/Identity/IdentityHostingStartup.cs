using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CarMarket.Web.Areas.Identity.IdentityHostingStartup))]

namespace CarMarket.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
