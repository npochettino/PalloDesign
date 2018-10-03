using System.Collections.Generic;
using System.Linq;
using Modelos;
using System.Data.Entity;

namespace DAL.Repositorio
{
    public class SucursalesRepositorio : Base
    {
        public SucursalesRepositorio()
            : base()
        {
        }

        public List<Sucursal> GetAll()
        {
            return _applicationDbContext.Sucursales.ToList();
        }

        public Sucursal GetOne(int id)
        {
            return _applicationDbContext.Sucursales.Find(id);
        }

        public bool Add(Sucursal sucursal)
        {
            try
            {
                _applicationDbContext.Sucursales.Add(sucursal);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Sucursal sucursal)
        {
            try
            {
                _applicationDbContext.Entry(sucursal).State = EntityState.Modified;
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int Count ()
        {
            return _applicationDbContext.Sucursales.Count();
        }

    }
}

