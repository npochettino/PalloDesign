using System;
using System.Collections.Generic;
using Modelos;
using System.ComponentModel.DataAnnotations;

namespace UI.Web.ViewModels.DevolucionesSinTicket
{
    public class DevolucionSinTicketIndexViewModel
    {
        public DevolucionSinTicketIndexViewModel()
        {
            DevolucionesSinTicket = new List<DevolucionSinTicket>();
        }

        public List<DevolucionSinTicket> DevolucionesSinTicket { get; set; }

        [Display(Name = "Fecha desde")]
        public DateTime FechaDesde { get; set; }

        [Display(Name = "Fecha hasta")]
        public DateTime FechaHasta { get; set; }
    }
}