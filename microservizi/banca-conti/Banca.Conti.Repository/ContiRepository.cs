using Banca.Conti.Repository.Abstraction;
using Banca.Conti.Repository.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banca.Conti.Repository
{
    public class ContiRepository(ContiDbContext dbContext) : IContiRepository
    {
        public async Task<Conto?> GetContoByIbanAsync(string iban, CancellationToken cancellationToken = default)
        {
            return await dbContext.Conti.FirstOrDefaultAsync(c => c.Iban == iban, cancellationToken);
        }

        public async Task CreaContoAsync(Conto conto, CancellationToken cancellationToken = default)
        {
            await dbContext.Conti.AddAsync(conto, cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
