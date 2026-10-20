using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banca.Conti.Repository.Model;

namespace Banca.Conti.Repository.Abstraction
{
   

    public interface IContiRepository
    {
        Task<Conto?> GetContoByIbanAsync(string iban, CancellationToken cancellationToken = default);
        Task CreaContoAsync(Conto conto, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
