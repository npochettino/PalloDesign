using DAL.Repositorio;
using Modelos;
using System.Collections.Generic;

namespace Servicios
{
    public class ChequesServicios
    {
        private ChequesRepositorio _chequesRepositorio;

        public ChequesServicios()
        {
            _chequesRepositorio = new ChequesRepositorio();
        }

        public List<Cheque> GetAll()
        {
            return _chequesRepositorio.GetAll();
        }

        public List<Cheque> GetAllSinCobrar()
        {
            return _chequesRepositorio.GetAllSinCobrar();
        }

        public Cheque GetOne(int id)
        {
            return _chequesRepositorio.GetOne(id);
        }

        public bool Add(Cheque cheque)
        {
            return _chequesRepositorio.Add(cheque);
        }

        public bool Delete(Cheque cheque)
        {
            return _chequesRepositorio.Delete(cheque);
        }

        public bool Update(Cheque cheque)
        {
            return _chequesRepositorio.Update(cheque);
        }

        public List<Cheque> GetByNumberOrBankOrSigner(string term)
        {
            return _chequesRepositorio.GetByNumberOrBankOrSigner(term);
        }
    }
}
