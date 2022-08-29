using Autofac;
using Autofac.Extensions.DependencyInjection;
using Local.Installers;
using Local.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.MyInstallerExtensions(builder);



//builder.Services.AddControllers();


//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

////custom DI
//builder.Services.AddDbContext<LocaldbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalContext")));

////Cors
//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//var MyAllowedAnyOrigins = "_myAllowedOrigins";

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//                      policy =>
//                      {
//                          policy.WithOrigins("https://www.w3schools.com",
//                                              "http://www.contoso.com")
//                          .AllowAnyHeader()
//                          .AllowAnyMethod();
//                          //.WithMethods("POST","PUT0");
//                      });
//    options.AddPolicy(name: MyAllowedAnyOrigins,
//                      policy =>
//                      {
//                          policy.WithOrigins("https://www.w3schools.com",
//                                              "http://www.contoso.com")
//                          .AllowAnyHeader()
//                          .AllowAnyMethod();
//                          //.WithMethods("POST","PUT0");
//                      });
//});


////////builder.Services.AddTransient<IProductService,ProductService>();
////////ใช้ AutoRefac ลงทะเบียนโดยอัตโนมัติกรณีมีหลายๆ Service
//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(containerBuilder =>
//{
//    containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
//    .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Test"))
//    .AsImplementedInterfaces();
//}));


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();


app.UseStaticFiles();

app.UseHttpsRedirection();


//app.UseCors(MyAllowSpecificOrigins);
app.UseCors(CORSInstaller.MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();


app.UseAuthorization();

app.MapControllers();

app.Run();


