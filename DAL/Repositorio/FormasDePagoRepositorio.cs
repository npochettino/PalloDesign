using System.Collections.Generic;
using System.Linq;
using Modelos;

namespace DAL.Repositorio
{
    public class FormasDePagoRepositorio : Base
    {
        public FormasDePagoRepositorio()
            : base()
        {
        }

        public List<FormaDePago> GetAll()
        {
            return _applicationDbContext.FormasDePago.ToList();
        }

        public FormaDePago GetOne(int id)
        {
            return _applicationDbContext.FormasDePago.Find(id);
        }

    }
}
