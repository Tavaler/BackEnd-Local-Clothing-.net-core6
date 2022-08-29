using Microsoft.EntityFrameworkCore;
using Local.Models.Data;

namespace Local.Installers
{
    public class DatabaseInstaller : IInstallers
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<LocaldbContext>(options => options.UseSqlServer(
             builder.Configuration.GetConnectionString("LocalContext")
             ));
        }
    }
}
