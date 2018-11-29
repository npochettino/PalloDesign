using Modelos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositorio
{
    public class ChequesRepositorio : Base
    {
        public ChequesRepositorio() : base()
        {
        }

        public List<Cheque> GetAllSinCobrar()
        {
            return _applicationDbContext.Cheques.Where(c => c.Cobrado == false).ToList();
        }

        public List<Cheque> GetAll()
        {
            return _applicationDbContext.Cheques.ToList();
        }

        public Cheque GetOne(int id)
        {
            return _applicationDbContext.Cheques.Find(id);
        }

        public bool Add(Cheque cheque)
        {
            try
            {
                _applicationDbContext.Cheques.Add(cheque);
                Guardar();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool Delete(Cheque cheque)
        {
            try
            {
                _applicationDbContext.Cheques.Remove(cheque);
                Guardar();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(Cheque cheque)
        {
            try
            {
                _applicationDbContext.Entry(cheque).State = EntityState.Modified;
                Guardar();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Cheque> GetByNumberOrBankOrSigner(string term)
        {
            return _applicationDbContext.Cheques.Where(a => (a.Numero.Contains(term) || a.Banco.Contains(term) || a.Firmante.Contains(term)) && a.Cobrado == false).ToList();
        }
    }
}
