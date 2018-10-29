using Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Web.ViewModels.Cheques
{
    public class ChequeEliminarViewModel
    {
        public ChequeEliminarViewModel()
        {
        }

        public int Id { get; set; }
        public string Numero { get; set; }
        public string Banco { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime FechaVencimiento { get; set; }
        public decimal Monto { get; set; }
        public string Firmante { get; set; }
        public bool Cobrado { get; set; }

        public ChequeEliminarViewModel(Cheque cheque)
        {
            Id = cheque.Id;
            Numero = cheque.Numero;
            Banco = cheque.Banco;
            FechaVencimiento = cheque.FechaVencimiento;
            Monto = cheque.Monto;
            Firmante = cheque.Firmante;
            Cobrado = cheque.Cobrado;
        }
    }
}