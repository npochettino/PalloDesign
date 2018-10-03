using System.Collections.Generic;
using Modelos;
using System;

namespace UI.Web.ViewModels.StockMovimientos
{
    public class StockMovimientoIndexViewModel
    {
        public StockMovimientoIndexViewModel()
        {
            StockMovimientos = new List<StockMovimiento>();
        }

        public List<StockMovimiento> StockMovimientos { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public int SucursalID { get; set; }

    }
}