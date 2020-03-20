using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;
using ServiceLayer.DTO;
using ServiceLayer.Services;
using System.Threading.Tasks;

namespace Orhedge
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IWebHost host = CreateWebHostBuilder(args).Build();
            await CreateRootUser(host);
            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            }).UseNLog().UseStartup<Startup>().ConfigureServices(services => services.AddAutofac());

        private async static Task CreateRootUser(IWebHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IStudentManagmentService studMngService =
                    scope.ServiceProvider.GetRequiredService<IStudentManagmentService>();
                IConfiguration config = host.Services.GetRequiredService<IConfiguration>();
                RegisterRootDTO rootDto = new RegisterRootDTO();
                config.GetSection("RootUser").Bind(rootDto);

                if (!await studMngService.IsStudentRegistered(rootDto.Email))
                    await studMngService.RegisterRootUser(rootDto);
            }
        }
    }
}
