using System;

namespace Modelos
{
    public class CierreCaja : Base
    {
        public DateTime FechaCierreCaja { get; set; }
        public DateTime FechaCierreEfectiva { get; set; }
        public decimal TotalVentasEfectivo { get; set; }
        public decimal TotalProveedores { get; set; }
        public decimal TotalSueldos { get; set; }
        public decimal TotalVentasTarjetas { get; set; }
        public decimal TotalVarios { get; set; }
        public decimal TotalVentas
        {
            get
            {
                return TotalVentasEfectivo + TotalVentasTarjetas;
            }
        }
        public decimal TotalGastos
        {
            get
            {
                return TotalVarios + TotalProveedores;
            }
        }
        public decimal Saldo { get; set; }
        public int TurnoID { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public int SucursalID { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int UsuarioID { get; set; }
        public decimal TotalCaja
        {
            get
            {
                return Saldo + TotalSueldos + TotalProveedores + TotalVentas - TotalVentasTarjetas;
            }
        }
    }
}
