using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelos;

namespace UI.Web.ViewModels.MovimientosEfectivo
{
    public class MovimientoEfectivoEliminarViewModel
    {
        public MovimientoEfectivoEliminarViewModel()
        {

        }

        public int Id { get; set; }
        public string NombreTipoMovimiento { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public string NombreFormaPago { get; set; }
        

        public MovimientoEfectivoEliminarViewModel(MovimientoEfectivo movimieto)
        {
            Id = movimieto.Id;
            NombreTipoMovimiento = movimieto.TipoMovimiento.Nombre;
            Descripcion = movimieto.Descripcion;
            Monto = movimieto.Monto;
            NombreFormaPago = movimieto.FormaDePago.Nombre;
        }
    }
}