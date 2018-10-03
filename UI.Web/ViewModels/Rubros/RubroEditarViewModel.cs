using Modelos;
using System.ComponentModel.DataAnnotations;

namespace UI.Web.ViewModels.Rubros
{
    public class RubroEditarViewModel
    {
        public RubroEditarViewModel()
        {
        }

        public RubroEditarViewModel(Rubro rubro)
        {
            Id = rubro.Id;
            Nombre = rubro.Nombre;
            Descripcion = rubro.Descripcion;

        }

        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Nombre es Requerido")]
        [StringLength(50, ErrorMessage = "El campo Nombre debe ser una cadena con una longitud máxima de 50")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(50, ErrorMessage = "El campo Descripción debe ser una cadena con una longitud máxima de 50")]
        public string Descripcion { get; set; }

        public Rubro Mapear()
        {
            Rubro Rubro = new Rubro();

            Rubro.Id = Id;
            Rubro.Nombre = Nombre;
            Rubro.Descripcion = Descripcion;
            Rubro.Habilitado = true;

            return Rubro;
        }
    }
}