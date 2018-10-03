using Modelos;
using System.Collections.Generic;

namespace UI.Web.ViewModels.Stock
{

    public class AsignarStockViewModel
    {
        public int ArticuloID { get; set; }
        public List<Sucursal> Sucursales { get; set; }
        public List<LineaAsignarStockViewModel> ListaArticulosAsignarStock { get; set; }
      // public List<StockArticuloSucursalViewModel> ListaArticulosAsignarStock { get; set; }
        public AsignarStockViewModel()
        {
            Sucursales = new List<Sucursal>();
            ListaArticulosAsignarStock = new List<LineaAsignarStockViewModel>();
        }
    }
}