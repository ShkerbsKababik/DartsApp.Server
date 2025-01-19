using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;
using DartsDbScheme.Contexts;
using DartsApp.Server.Facades.UserService;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using System.Data.Common;
public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DartsDbContext>));
            if (dbContextDescriptor != null)
            {
                services.Remove(dbContextDescriptor);
            }

            services.AddDbContext<DartsDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });
        });
    }
}