using System;
using System.ComponentModel.DataAnnotations;
using Modelos;

namespace UI.Web.ViewModels.Sueldos
{
    public class SueldoEncargadoViewModel
    {
        [Display(Name = "Encargado")]
        [Required(ErrorMessage = "Encargado es Requerido")]
        public string Encargado { get; set; }

        [Display(Name = "Horas Trabajadas")]
        [Required(ErrorMessage = "Horas Trabajadas es Requerido")]
        [Range(0.01, 100000000.00, ErrorMessage = "Debe ingresar un numero mayor a cero")]
        public decimal Horas { get; set; }

        [Display(Name = "Precio por Hora")]
        [Required(ErrorMessage = "Precio por Hora es Requerido")]
        [Range(0.01, 100000000.00, ErrorMessage = "Debe ingresar un numero mayor a cero")]
        public decimal PrecioHora { get; set; }

        //[Display(Name = "Sueldo")]
        //[Required(ErrorMessage = "Sueldo es Requerido")]
        //[Range(0.01, 100000000.00, ErrorMessage = "Debe ingresar un numero mayor a cero")]
        //public decimal Sueldo { get; set; }

        public MovimientoEfectivo Mapear()
        {
            MovimientoEfectivo MovimientoEfectivo = new MovimientoEfectivo();

            MovimientoEfectivo.Fecha = DateTime.Now;
            MovimientoEfectivo.Descripcion = "Pago Sueldo al Encargado " + Encargado;
            MovimientoEfectivo.Monto = Horas * PrecioHora;
            MovimientoEfectivo.TipoMovimientoID = 2; //Pago Sueldo Encargado
            var usuario = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
            MovimientoEfectivo.UsuarioID = usuario.Id;
            MovimientoEfectivo.SucursalID = (int)System.Web.HttpContext.Current.Session["SucursalActual"];

            return (MovimientoEfectivo);
        }
    }
}