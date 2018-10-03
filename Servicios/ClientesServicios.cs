using DAL.Repositorio;
using Modelos;
using System.Collections.Generic;

namespace Servicios
{
    public class ClientesServicios
    {
        private ClientesRepositorio _clientesRepositorio;

        public ClientesServicios()
        {
            _clientesRepositorio = new ClientesRepositorio();
        }

        public List<Cliente> GetAll()
        {
            return _clientesRepositorio.GetAll();
        }

        public List<Cliente> GetAllHabilitados()
        {
            return _clientesRepositorio.GetAllHabilitados();
        }

        public Cliente GetOne(int id)
        {
            return _clientesRepositorio.GetOne(id);
        }

        public bool Add(Cliente cliente)
        {
            return _clientesRepositorio.Add(cliente);
        }

        public bool Delete(Cliente cliente)
        {
            return _clientesRepositorio.Delete(cliente);
        }

        public bool Update(Cliente cliente)
        {
            return _clientesRepositorio.Update(cliente);
        }

        public List<Cliente> GetByNameOrDNI(string term)
        {
            return _clientesRepositorio.GetByNameOrDNI(term);
        }
    }
}
