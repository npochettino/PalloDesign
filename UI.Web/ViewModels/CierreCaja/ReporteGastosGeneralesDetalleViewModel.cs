using System;

namespace UI.Web.ViewModels.CierreCaja
{
    public class ReporteGastosGeneralesDetalleViewModel
    {
        public DateTime Fecha { get; set; }
        public decimal TotalGastos { get; set; }
        public decimal TotalSueldos { get; set; }
        public decimal TotalVentasEfectivo { get; set; }
        public decimal TotalIngresosPorTarjeta { get; set; }
    }
}