using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;

namespace Local.Installers
{
        public class ServiceInstaller : IInstallers
        {
            public void InstallServices(WebApplicationBuilder builder)
            {
                //builder.Services.AddTransient<IProductService,ProductService>();
                //ใช้ AutoRefac ลงทะเบียนโดยอัตโนมัติกรณีมีหลายๆ Service
                builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(containerBuilder =>
                {
                    containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                    .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Test"))
                    .AsImplementedInterfaces();
                }));
            }
        }

}
