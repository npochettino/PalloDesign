using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Web.ViewModels.CierreCaja
{
    public class ReporteGananciasResumenViewModel
    {
        public decimal TotalGastos { get; set; }
        public decimal TotalIngresos { get; set; }
        public decimal TotalSueldos { get; set; }
        public decimal TotalVentasEfectivo { get; set; }
        public decimal TotalVentasCC { get; set; }
        public decimal TotalVentasCheque { get; set; }
        public decimal TotalIngresosPorTarjeta { get; set; }

        public decimal TotalesTotal
        {
            get
            {
                return (TotalVentasEfectivo + TotalVentasCC + TotalVentasCheque + TotalIngresos + TotalIngresosPorTarjeta - TotalSueldos - TotalGastos);
            }
        }
    }
}