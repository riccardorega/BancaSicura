using System.Net;
using System.Net.Http.Json;
using Banca.Conti.ClientHttp.Abstraction;
using Banca.Conti.Shared;

namespace Banca.Conti.ClientHttp;

public class ClientHttp(HttpClient httpClient) : IClientHttp
{
    public async Task<IEnumerable<ContoDto>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/api/Conti", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<ContoDto>>(cancellationToken: cancellationToken);
    }

    public async Task<ContoDto?> GetByIbanAsync(string iban, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/api/Conti/{Uri.EscapeDataString(iban)}", cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ContoDto>(cancellationToken: cancellationToken);
    }

    public async Task<ContoDto?> CreateAsync(ContoDto dto, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/api/Conti", dto, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ContoDto>(cancellationToken: cancellationToken);
    }

    public async Task<bool> UpdateAsync(ContoDto dto, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/api/Conti/{Uri.EscapeDataString(dto.Iban)}", dto, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return false;
        }

        response.EnsureSuccessStatusCode();
        return true;
    }
}