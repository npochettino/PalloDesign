using Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Web.ViewModels.Ventas
{
    public class ReportePorRubroViewModel
    {
        [Display(Name = "Fecha desde")]
        public DateTime FechaDesde { get; set; }

        [Display(Name = "Fecha hasta")]
        public DateTime FechaHasta { get; set; }        

        public List<ReportePorRubroDetalleViewModel> Detalles { get; set; }

        public string CabeceraReporte { get; set; }
    }
}