using CloneWhatsBE.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CloneWhatsBE.Configurations;

public static class BearerJwtConfig
{
    public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var jwtSettingsOptionsSection = configuration.GetRequiredSection(JwtSettingsOptions.SectionName);
            var jwtSettingsOptions = jwtSettingsOptionsSection.Get<JwtSettingsOptions>();

            var secretByte = Encoding.UTF8.GetBytes(jwtSettingsOptions!.Secret);
            var secretKey = new SymmetricSecurityKey(secretByte);

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = secretKey,
                ValidateLifetime = false,
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });

        return services;
    }
}
