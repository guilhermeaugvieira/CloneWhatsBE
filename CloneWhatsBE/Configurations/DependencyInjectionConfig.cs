using CloneWhatsBE.Auth;
using Microsoft.Extensions.Options;

namespace CloneWhatsBE.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Jwt Settings Globally
        services.Configure<JwtSettingsOptions>(configuration.GetRequiredSection(JwtSettingsOptions.SectionName));
        services.AddSingleton(provider => provider.GetRequiredService<IOptions<JwtSettingsOptions>>().Value);

        return services;
    }
}
