using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos
{
    public class StockMovimiento:Base
    {
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }

        public int TipoMovimientoStockID { get; set; }
        public virtual TipoMovimientoStock Motivo { get; set; }
        
        public int ArticuloID { get; set; }
        public virtual Articulo Articulo { get; set; }

        public int SucursalID { get; set; }
        public virtual Sucursal Sucursal { get; set; }

        public int UsuarioID { get; set; }
        public virtual Usuario Usuario { get; set; }

        public int StockActual { get; set; }
    }
}
