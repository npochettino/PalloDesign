using System.ComponentModel.DataAnnotations;

namespace UI.Web.ViewModels.Precios

{
    public class PrecioActualizarPorRubroViewModel
    {
        [Display(Name = "Porcentaje de Actualización (%)")]
        [Required(ErrorMessage = "Porcentaje de Actualización es Requerido")]
        public int PorcentajeActualizacion{ get; set; }

        [Display(Name = "Rubro")]
        [Required(ErrorMessage = "Rubro es Requerido")]
        public int RubroID { get; set; }
        
    }
}