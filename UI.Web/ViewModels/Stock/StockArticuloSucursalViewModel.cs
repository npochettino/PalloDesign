using Modelos;

namespace UI.Web.ViewModels.Stock
{

    public class StockArticuloSucursalViewModel
    {
        public int ArticuloID { get; set; }
        public virtual Articulo Articulo { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public int SucursalID { get; set; }
        public decimal StockActual { get; set; }
        public decimal StockAgregar { get; set; }
    }
}