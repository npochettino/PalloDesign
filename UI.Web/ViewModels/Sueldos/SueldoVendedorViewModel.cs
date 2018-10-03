using System;
using System.ComponentModel.DataAnnotations;
using Modelos;

namespace UI.Web.ViewModels.Sueldos
{
    public class SueldoVendedorViewModel
    {
        [Display(Name = "Vendedor")]
        [Required(ErrorMessage = "Vendedor es Requerido")]
        public string Vendedor { get; set; }

        [Display(Name = "Horas Trabajadas")]
        [Required(ErrorMessage = "Horas Trabajadas es Requerido")]
        [Range(0.01, 100000000.00, ErrorMessage = "Debe ingresar un numero mayor a cero")]
        public decimal Horas { get; set; }

        [Display(Name = "Precio por Hora")]
        [Required(ErrorMessage = "Precio por Hora es Requerido")]
        [Range(0.01, 100000000.00, ErrorMessage = "Debe ingresar un numero mayor a cero")]
        public decimal PrecioHora { get; set; }

        public MovimientoEfectivo Mapear()
        {
            MovimientoEfectivo MovimientoEfectivo = new MovimientoEfectivo();

            MovimientoEfectivo.Fecha = DateTime.Now;
            MovimientoEfectivo.Descripcion = "Pago Sueldo al Vendedor "+Vendedor;
            MovimientoEfectivo.Monto = Horas * PrecioHora;
            MovimientoEfectivo.TipoMovimientoID = 1; //Pago Sueldo Vendedor
            var usuario = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
            MovimientoEfectivo.UsuarioID = usuario.Id;
            MovimientoEfectivo.SucursalID = (int)System.Web.HttpContext.Current.Session["SucursalActual"];

            return (MovimientoEfectivo); 
        }
    }
}