using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Local.Settings;

namespace Local.Installers
{
    public class JWTInstaller : IInstallers
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            var Jwtsetting   = new JwtSetting();
            builder.Services.AddSingleton(Jwtsetting);
            // Stock WorkShop-JWT page-61
            //อธิบาย https://docs.microsoft.com/en-us/answers/questions/688116/how-to-implement-jwt-autentication-in-asp-core-net.html

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = Jwtsetting.Issuer,
                            ValidateAudience = true,
                            ValidAudience = Jwtsetting.Audience,
                            ValidateIssuerSigningKey = true,
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwtsetting.Key)), 
                            ClockSkew = TimeSpan.Zero
                        };
                });

        }
    }

}
