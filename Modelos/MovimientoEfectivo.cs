using System;

namespace Modelos
{
    public class MovimientoEfectivo:Base
    {
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }

        public int TipoMovimientoID { get; set; }
        public virtual TipoMovimientoEfectivo TipoMovimiento { get; set; }

        public int UsuarioID { get; set; }
        public virtual Usuario Usuario { get; set; }

        public int SucursalID { get; set; }
        public virtual Sucursal Sucursal { get; set; }

        public int FormaDePagoID { get; set; }
        public virtual FormaDePago FormaDePago { get; set; }

    }
}
