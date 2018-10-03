using System.Collections.Generic;
using Modelos;

namespace UI.Web.ViewModels.TiposMovimientosEfectivo

{
    public class TipoMovimientoEfectivoIndexViewModel
    {
        public TipoMovimientoEfectivoIndexViewModel()
        {
            TiposMovimientosCaja = new List<TipoMovimientoEfectivo>();
        }

        public List<TipoMovimientoEfectivo> TiposMovimientosCaja { get; set; }

    }
}