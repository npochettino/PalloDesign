namespace Modelos
{
    public class StockArticuloSucursal : Base
    {
        public int ArticuloID { get; set; }
        public virtual Articulo Articulo { get; set; }
        public int SucursalID { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public decimal StockActual { get; set; }
    }
}
