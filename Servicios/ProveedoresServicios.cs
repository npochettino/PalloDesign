using DAL.Repositorio;
using Modelos;
using System.Collections.Generic;

namespace Servicios
{
    public class ProveedoresServicios
    {
        private ProveedoresRepositorio _proveedoresRepositorio;

        public ProveedoresServicios()
        {
            _proveedoresRepositorio = new ProveedoresRepositorio();
        }

        public List<Proveedor> GetAll()
        {
            return _proveedoresRepositorio.GetAll();
        }

        public Proveedor GetOne(int id)
        {
            return _proveedoresRepositorio.GetOne(id);
        }

        public bool Add(Proveedor proveedor)
        {
            return _proveedoresRepositorio.Add(proveedor);
        }

        public bool Delete(Proveedor proveedor)
        {
            return _proveedoresRepositorio.Delete(proveedor);
        }

        public bool Update(Proveedor proveedor)
        {
            return _proveedoresRepositorio.Update(proveedor);
        }
    }
}
