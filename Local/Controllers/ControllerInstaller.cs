using Local.Installers;
using Microsoft.AspNetCore.Mvc;

namespace Local.Controllers
{
    public interface ControllerInstaller : IInstallers
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
        }
    }
}
