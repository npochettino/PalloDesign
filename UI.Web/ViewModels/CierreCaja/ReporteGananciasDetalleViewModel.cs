using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Web.ViewModels.CierreCaja
{
    public class ReporteGananciasDetalleViewModel
    {
        public DateTime Fecha { get; set; }

        public string Tipo { get; set; }

        public decimal Importe { get; set; }
    }
}