using System.Collections.Generic;
using Modelos;

namespace UI.Web.ViewModels.MovimientosEfectivo
{
    public class MovimientoEfectivoIndexViewModel
    {
        public MovimientoEfectivoIndexViewModel()
        {
            Movimientos = new List<MovimientoEfectivo>();
        }

        public List<MovimientoEfectivo> Movimientos { get; set; }
        
    }
}