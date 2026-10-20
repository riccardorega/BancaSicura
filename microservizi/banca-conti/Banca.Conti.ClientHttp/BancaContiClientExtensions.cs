using Banca.Conti.ClientHttp;
using Banca.Conti.ClientHttp.Abstraction;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class BancaContiClientExtensions
{
    public static IServiceCollection AddBancaContiClient(this IServiceCollection services, IConfiguration configuration)
    {
        var confSection = configuration.GetSection(BancaContiClientOptions.SectionName);
        var options = confSection.Get<BancaContiClientOptions>() ?? new();

        services.AddHttpClient<IClientHttp, ClientHttp>(client =>
        {
            client.BaseAddress = new Uri(options.BaseAddress);
        }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            // Bypassa certificati self-signed per i test di sviluppo locale
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true
        });

        return services;
    }
}