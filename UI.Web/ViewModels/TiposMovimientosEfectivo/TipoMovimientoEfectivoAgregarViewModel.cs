using Modelos;
using System.ComponentModel.DataAnnotations;

namespace UI.Web.ViewModels.TiposMovimientosEfectivo

{
    public class TipoMovimientoEfectivoAgregarViewModel
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Nombre es Requerido")]
        [StringLength(50, ErrorMessage = "El campo Nombre debe ser una cadena con una longitud máxima de 50")]
        public string Nombre { get; set; }

        public bool Caja { get; set; }

        public bool Suma { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "Categoría es Requerido")]
        public int CategoriaMovimientoEfectivoID { get; set; }

        public TipoMovimientoEfectivo Mapear()
        {
            TipoMovimientoEfectivo TipoMovimientoCaja = new TipoMovimientoEfectivo();

            TipoMovimientoCaja.Nombre = Nombre;
            TipoMovimientoCaja.Caja = Caja;
            TipoMovimientoCaja.Suma = Suma;
            TipoMovimientoCaja.CategoriaMovimientoEfectivoID = CategoriaMovimientoEfectivoID;
            TipoMovimientoCaja.Habilitado = true;

            return TipoMovimientoCaja;
        }
    }
}