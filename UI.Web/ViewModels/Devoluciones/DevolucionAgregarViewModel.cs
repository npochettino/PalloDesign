using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UI.Web.ViewModels.Devoluciones
{
    public class DevolucionAgregarViewModel
    {
        public DevolucionAgregarViewModel()
        {
            Items = new List<DevolucionItemViewModel>();
        }

        [Required(ErrorMessage = "Código de Venta es Requerido")]
        [Display(Name = "Ingrese el código de la Venta: ")]
        public int VentaID { get; set; }

        public List<DevolucionItemViewModel> Items { get; set; }
    }
}