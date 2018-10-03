using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Web.ViewModels.Ventas
{
    public class ReportePorRubroDetalleViewModel
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }

        public int Total { get; set; }

        public string Porcentaje
        {
            get
            {
                int current = Cantidad;
                int maximum = Total;
                var percent = ((double)(current) / maximum);
                return percent.ToString("0.0%");
            }
        }
    }
}