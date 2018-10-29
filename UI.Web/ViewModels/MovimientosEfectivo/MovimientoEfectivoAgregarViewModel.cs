using Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UI.Web.ViewModels.MovimientosEfectivo

{
    public class MovimientoEfectivoAgregarViewModel
    {
        [Display(Name = "Tipo de Movimiento")]
        [Required(ErrorMessage = "Tipo de Movimiento es Requerido")]
        public int TipoMovimientoID { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(50, ErrorMessage = "El campo Descripción debe ser una cadena con una longitud máxima de 50")]
        public string Descripcion { get; set; }

        [Display(Name = "Monto")]
        [Required(ErrorMessage = "Monto es Requerido")]
        [Range(1.00, 100000000.00, ErrorMessage = "Monto Incorrecto. Por favor ingrese un monto válido.")]
        public decimal Monto { get; set; }

        [Display(Name = "Forma de Pago")]
        public int FormaDePagoID { get; set; }
        public SelectList FormasDePago { get; set; }

        public MovimientoEfectivo Mapear()
        {
            MovimientoEfectivo Movimiento = new MovimientoEfectivo();

            Movimiento.Fecha = DateTime.Now;
            Movimiento.Descripcion = Descripcion;
            Movimiento.Monto = Monto;
            Movimiento.TipoMovimientoID = TipoMovimientoID;
            var usuario = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
            Movimiento.UsuarioID = usuario.Id;
            Movimiento.SucursalID = (int)System.Web.HttpContext.Current.Session["SucursalActual"];
            Movimiento.FormaDePagoID = FormaDePagoID;

            return Movimiento;
        }
    }
}