using Banca.Conti.Shared;

namespace Banca.Conti.Business;

public interface IContiService
{
    Task<IEnumerable<ContoDto>> GetAllAsync();
    Task<ContoDto?> GetByIbanAsync(string iban);
    Task<ContoDto> CreateAsync(ContoDto dto);
    Task<bool> UpdateAsync(ContoDto dto);
}