using Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Web.ViewModels.CierreCaja
{
    public class ReporteGananciasViewModel
    {
        [Display(Name = "Desde ")]
        public DateTime FechaDesde { get; set; }
        [Display(Name = "Hasta ")]
        public DateTime FechaHasta { get; set; }

        public Sucursal Sucursal { get; set; }

        public string CabeceraReporte { get; set; }

        public ReporteGananciasResumenViewModel Resumen { get; set; }

        public List<ReporteGananciasDetalleViewModel> Detalles { get; set; }
    }
}