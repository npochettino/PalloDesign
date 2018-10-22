using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Cheque:Base
    {
        public string Numero { get; set; }
        public string Banco { get; set; }
        public virtual Cliente Cliente { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Monto { get; set; }
        public string Firmante { get; set; }
        public bool Cobrado { get; set; }
    }
}
