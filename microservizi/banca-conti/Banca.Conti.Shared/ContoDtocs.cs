using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Banca.Conti.Shared
{

    public class ContoDto
    {
        public int Id { get; set; }
        public string Iban { get; set; } = string.Empty;
        public string Intestatario { get; set; } = string.Empty;
        public float SaldoContabile { get; set; }
        public float SaldoDisponibile { get; set; }
    }
}
