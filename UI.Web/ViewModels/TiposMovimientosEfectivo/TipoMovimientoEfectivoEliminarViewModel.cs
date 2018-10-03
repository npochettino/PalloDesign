using Modelos;

namespace UI.Web.ViewModels.TiposMovimientosEfectivo
{
    public class TipoMovimientoEfectivoEliminarViewModel
    {
        public TipoMovimientoEfectivoEliminarViewModel()
        {
        }

        public TipoMovimientoEfectivoEliminarViewModel(TipoMovimientoEfectivo tipoMovimientoCaja)
        {
            Id = tipoMovimientoCaja.Id;
            Nombre = tipoMovimientoCaja.Nombre;
            Caja = tipoMovimientoCaja.Caja;
            Suma = tipoMovimientoCaja.Suma;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Caja { get; set; }
        public bool Suma { get; set; }

    }
}