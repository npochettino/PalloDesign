﻿using Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UI.Web.ViewModels.Ventas
{
    public class ReportePorVendedorViewModel
    {
        [Display(Name = "Fecha desde")]
        public DateTime FechaDesde { get; set; }

        [Display(Name = "Fecha hasta")]
        public DateTime FechaHasta { get; set; }

        public List<Venta> Ventas { get; set; }

        [Display(Name = "Vendedor")]
        public int VendedorID { get; set; }    

        public string CabeceraReporte { get; set; }

        public decimal Total
        {
            get
            {
                try { return Ventas.Sum(a => a.TotalVenta); }
                catch { return 0; }

            }
        }
    }
}