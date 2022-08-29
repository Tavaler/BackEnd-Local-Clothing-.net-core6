namespace Local.Installers
{
    public class CORSInstaller : IInstallers
    {

        static public string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                      //.WithMethods("POST","PUT0");
                                  });
            });
        }

    }
}
