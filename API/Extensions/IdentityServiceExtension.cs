using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extension;

public static class IdentityServiceExtension {
    public static IServiceCollection AddIdentityServices(this IServiceCollection services,
    IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(option =>
            {
                var tokenkey = config["TokenKey"] ?? throw new Exception("Jwt Token not found");
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        return services;   
    }

}