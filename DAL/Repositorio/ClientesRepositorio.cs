using System.Collections.Generic;
using System.Linq;
using Modelos;
using System.Data.Entity;

namespace DAL.Repositorio
{
    public class ClientesRepositorio : Base
    {
        public ClientesRepositorio()
            : base()
        {
        }

        public List<Cliente> GetAllHabilitados()
        {
            return _applicationDbContext.Clientes.Where(a => a.Habilitado == true).ToList();
        }

        public List<Cliente> GetAll()
        {
            return _applicationDbContext.Clientes.ToList();
        }

        public Cliente GetOne(int id)
        {
            return _applicationDbContext.Clientes.Find(id);
        }

        public bool Add(Cliente cliente)
        {
            try
            {
                _applicationDbContext.Clientes.Add(cliente);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Cliente cliente)
        {
            try
            {
                //_applicationDbContext.Clientes.Remove(cliente);
                cliente.Habilitado = false;
                _applicationDbContext.Entry(cliente).State = EntityState.Modified;
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Cliente> GetByNameOrDNI(string term)
        {
            return _applicationDbContext.Clientes.Where(a => (a.Apellido.Contains(term) || a.DNI.Contains(term)) && a.Habilitado == true).ToList();
        }

        public bool Update(Cliente cliente)
        {
            try
            {
                _applicationDbContext.Entry(cliente).State = EntityState.Modified;
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