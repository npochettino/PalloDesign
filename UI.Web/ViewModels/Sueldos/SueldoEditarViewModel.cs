using Modelos;
using System.ComponentModel.DataAnnotations;
using Foolproof;

namespace UI.Web.ViewModels.Sueldos
{
    public class SueldoEditarViewModel
    {

        public SueldoEditarViewModel()
        {
        }

        public SueldoEditarViewModel(MovimientoEfectivo MovimientoEfectivo)
        {
            Id = MovimientoEfectivo.Id;
            Descripcion = MovimientoEfectivo.Descripcion;
            Monto = MovimientoEfectivo.Monto;
        }

        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(50, ErrorMessage = "El campo Descripción debe ser una cadena con una longitud máxima de 50")]
        public string Descripcion { get; set; }

        [Display(Name = "Monto Final")]
        [Required(ErrorMessage = "Monto es Requerido")]
        [Range(1.00, 100000000.00, ErrorMessage = "Monto Incorrecto. Por favor ingrese un monto válido.")]
        public decimal Monto { get; set; }

        public MovimientoEfectivo Mapear()
        {
            MovimientoEfectivo MovimientoEfectivo = new MovimientoEfectivo();

            MovimientoEfectivo.Id = Id;
            MovimientoEfectivo.Descripcion = Descripcion;
            MovimientoEfectivo.Monto = Monto;

            return MovimientoEfectivo;
        }


    }
}