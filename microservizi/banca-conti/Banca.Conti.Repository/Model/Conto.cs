using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banca.Conti.Repository.Model
{
    public class Conto
    {
        public int Id { get; set; }
        public string Iban { get; set; } = string.Empty;
        public string Intestatario { get; set; } = string.Empty;
        public float SaldoContabile { get; set; }
        public float SaldoDisponibile { get; set; }
    }
}
