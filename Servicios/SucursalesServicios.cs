using DAL.Repositorio;
using Modelos;
using System.Collections.Generic;

namespace Servicios
{
    public class SucursalesServicios
    {
        private SucursalesRepositorio _sucursalesRepositorio;

        public SucursalesServicios()
        {
            _sucursalesRepositorio = new SucursalesRepositorio();
        }

        public List<Sucursal> GetAll()
        {
            return _sucursalesRepositorio.GetAll();
        }

        public Sucursal GetOne(int id)
        {
            return _sucursalesRepositorio.GetOne(id);
        }

        public bool Add(Sucursal sucursal)
        {
            return _sucursalesRepositorio.Add(sucursal);
        }

        public bool Update(Sucursal sucursal)
        {
            return _sucursalesRepositorio.Update(sucursal);
        }
    }
}
