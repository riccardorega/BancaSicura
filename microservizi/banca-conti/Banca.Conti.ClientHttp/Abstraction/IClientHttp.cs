using Banca.Conti.Shared;

namespace Banca.Conti.ClientHttp.Abstraction;

public interface IClientHttp
{
    Task<IEnumerable<ContoDto>?> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ContoDto?> GetByIbanAsync(string iban, CancellationToken cancellationToken = default);
    Task<ContoDto?> CreateAsync(ContoDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(ContoDto dto, CancellationToken cancellationToken = default);
}