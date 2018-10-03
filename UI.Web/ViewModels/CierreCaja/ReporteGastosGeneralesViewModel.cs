using Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UI.Web.ViewModels.CierreCaja
{
    public class ReporteGastosGeneralesViewModel
    {

        [Display(Name = "Desde ")]
        public DateTime FechaDesde { get; set; }
        [Display(Name = "Hasta ")]
        public DateTime FechaHasta { get; set; }

        public Sucursal Sucursal { get; set; }

        [Display(Name = "Total Caja")]
        public decimal TotalCaja { get; set; }

        public List<ReporteGastosGeneralesDetalleViewModel> Detalle { get; set; }

        public decimal TotalesGastos
        {
            get
            {
                return -Detalle.Sum(a => a.TotalGastos);
            }
        }

        public decimal TotalesSueldos
        {
            get
            {
                return -Detalle.Sum(a => a.TotalSueldos);
            }
        }

        public decimal TotalesVentas
        {
            get
            {
                return Detalle.Sum(a => a.TotalVentasEfectivo);
            }
        }

        public decimal TotalesTarjetas
        {
            get
            {
                return Detalle.Sum(a => a.TotalIngresosPorTarjeta);
            }
        }

        public decimal TotalesTotal
        {
            get
            {
                return (TotalesTarjetas + TotalesVentas + TotalesSueldos + TotalesGastos);
            }
        }

        public string CabeceraReporte { get; set; }

        public ReporteGastosGeneralesViewModel()
        {
            Detalle = new List<ReporteGastosGeneralesDetalleViewModel>();
        }
    }
}