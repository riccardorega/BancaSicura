using Banca.Conti.Repository;
using Banca.Conti.Repository.Model;
using Banca.Conti.Shared;
using Microsoft.EntityFrameworkCore;

namespace Banca.Conti.Business;

public class ContiService : IContiService
{
    private readonly ContiDbContext _context;

    public ContiService(ContiDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ContoDto>> GetAllAsync()
    {
        return await _context.Conti
            .AsNoTracking()
            .Select(c => new ContoDto
            {
                Id = c.Id,
                Iban = c.Iban,
                Intestatario = c.Intestatario,
                SaldoContabile = c.SaldoContabile,
                SaldoDisponibile = c.SaldoDisponibile
            })
            .ToListAsync();
    }

    public async Task<ContoDto?> GetByIbanAsync(string iban)
    {
        var entity = await _context.Conti
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Iban == iban);

        if (entity == null)
        {
            return null;
        }

        return new ContoDto
        {
            Id = entity.Id,
            Iban = entity.Iban,
            Intestatario = entity.Intestatario,
            SaldoContabile = entity.SaldoContabile,
            SaldoDisponibile = entity.SaldoDisponibile
        };
    }

    public async Task<ContoDto> CreateAsync(ContoDto dto)
    {
        // Regola di business: verifica unicità dell'IBAN
        var esisteGia = await _context.Conti.AnyAsync(c => c.Iban == dto.Iban);
        if (esisteGia)
        {
            throw new InvalidOperationException($"Esiste già un conto associato all'IBAN {dto.Iban}.");
        }

        var entity = new Conto
        {
            Iban = dto.Iban,
            Intestatario = dto.Intestatario,
            SaldoContabile = dto.SaldoContabile,
            SaldoDisponibile = dto.SaldoDisponibile
        };

        _context.Conti.Add(entity);
        await _context.SaveChangesAsync();

        dto.Id = entity.Id;
        return dto;
    }

    public async Task<bool> UpdateAsync(ContoDto dto)
    {
        var entity = await _context.Conti.FirstOrDefaultAsync(c => c.Iban == dto.Iban);
        if (entity == null)
        {
            return false;
        }

        // Aggiornamento campi consentiti
        entity.Intestatario = dto.Intestatario;
        entity.SaldoContabile = dto.SaldoContabile;
        entity.SaldoDisponibile = dto.SaldoDisponibile;

        await _context.SaveChangesAsync();
        return true;
    }
}