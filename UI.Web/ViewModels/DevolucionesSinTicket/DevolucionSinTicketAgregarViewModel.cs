using System;
using System.ComponentModel.DataAnnotations;
using Modelos;

namespace UI.Web.ViewModels.DevolucionesSinTicket
{
    public class DevolucionSinTicketAgregarViewModel
    {
        [Display(Name = "Artículo")]
        [Required(ErrorMessage = "Articulo es Requerido")]
        public int ArticuloID { get; set; }

        [Display(Name = "Artículo")]
        public string Articulo { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Cantidad es Requerido")]
        public int Cantidad { get; set; }

        public decimal Monto { get; set; }

        [Display(Name = "Motivo")]
        public string Motivo { get; set; }

        public bool RegresaAlStock { get; set; }

        public DevolucionSinTicket Mapear()
        {
            DevolucionSinTicket DevolucionSinTicket = new DevolucionSinTicket();

            DevolucionSinTicket.Fecha = DateTime.Now;
            DevolucionSinTicket.Motivo = Motivo;
            DevolucionSinTicket.RegresaAlStock = RegresaAlStock;
            DevolucionSinTicket.Cantidad = Cantidad;
            DevolucionSinTicket.Monto = Monto;
            DevolucionSinTicket.ArticuloID = ArticuloID;
            DevolucionSinTicket.SucursalID = int.Parse(System.Web.HttpContext.Current.Session["SucursalActual"].ToString());

            return DevolucionSinTicket;
        }
    }
}