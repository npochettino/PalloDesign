using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Web.ViewModels.Ventas
{
    public class ReportePorRubroDetalleViewModel
    {
        public string Nombre { get; set; }
        public decimal Cantidad { get; set; }

        public decimal Total { get; set; }

        public string Porcentaje
        {
            get
            {
                decimal current = Cantidad;
                decimal maximum = Total;
                var percent = ((decimal)(current) / maximum);
                return percent.ToString("0.0%");
            }
        }
    }
}