using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Modelos;

namespace UI.Web.ViewModels.Sueldos
{
    public class SueldoIndexViewModel
    {
        public SueldoIndexViewModel()
        {
            Sueldos = new List<MovimientoEfectivo>();
        }

        public List<MovimientoEfectivo> Sueldos { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
    }
}