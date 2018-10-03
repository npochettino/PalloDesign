using Modelos;
using System.ComponentModel.DataAnnotations;

namespace UI.Web.ViewModels.Sucursales
{
    public class SucursalEditarViewModel
    {
        public SucursalEditarViewModel()
        {
        }

        public SucursalEditarViewModel(Sucursal sucursal)
        {
            Id = sucursal.Id;
            Nombre = sucursal.Nombre;
            Descripcion = sucursal.Descripcion;

        }

        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Nombre es Requerido")]
        [StringLength(50, ErrorMessage = "El campo Nombre debe ser una cadena con una longitud máxima de 50")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(50, ErrorMessage = "El campo Descripción debe ser una cadena con una longitud máxima de 50")]
        public string Descripcion { get; set; }
        
        public Sucursal Mapear()
        {
            Sucursal Sucursal = new Sucursal();

            Sucursal.Id = Id;
            Sucursal.Nombre = Nombre;
            Sucursal.Descripcion = Descripcion;

            return Sucursal;
        }
    }
}