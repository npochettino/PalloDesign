using System.Collections.Generic;
using System.Linq;
using Modelos;
using System.Data.Entity;

namespace DAL.Repositorio
{
    public class ProveedoresRepositorio : Base
    {
        public ProveedoresRepositorio()
            : base()
        {
        }

        public List<Proveedor> GetAll()
        {
            return _applicationDbContext.Proveedores
                .Where(a => a.Habilitado == true).ToList();
        }

        public Proveedor GetOne(int id)
        {
            return _applicationDbContext.Proveedores.Find(id);
        }

        public bool Add(Proveedor proveedor)
        {
            try
            {
                _applicationDbContext.Proveedores.Add(proveedor);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Proveedor proveedor)
        {
            try
            {
                //_applicationDbContext.Proveedores.Remove(proveedor);
                proveedor.Habilitado = false;
                _applicationDbContext.Entry(proveedor).State = EntityState.Modified;
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Proveedor proveedor)
        {
            try
            {
                _applicationDbContext.Entry(proveedor).State = EntityState.Modified;
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
