using System.Collections.Generic;
using System.Linq;
using Modelos;
using System.Data.Entity;

namespace DAL.Repositorio
{
    public class RubrosRepositorio : Base
    {
        public RubrosRepositorio()
            : base()
        {
        }

        public List<Rubro> GetAll()
        {
            return _applicationDbContext.Rubros.Where(a => a.Habilitado == true).ToList();
        }

        public Rubro GetOne(int id)
        {
            return _applicationDbContext.Rubros.Find(id);
        }

        public bool Add(Rubro rubro)
        {
            try
            {
                _applicationDbContext.Rubros.Add(rubro);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Rubro rubro)
        {
            try
            {
                _applicationDbContext.Rubros.Remove(rubro);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Rubro rubro)
        {
            try
            {
                _applicationDbContext.Entry(rubro).State = EntityState.Modified;
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}

