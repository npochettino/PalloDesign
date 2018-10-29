using System.Collections.Generic;
using Modelos;

namespace UI.Web.ViewModels.Cheques
{
    public class ChequeIndexViewModel
    {
        public ChequeIndexViewModel()
        {
            Cheques = new List<Cheque>();
        }

        public List<Cheque> Cheques { get; set; }
    }
}