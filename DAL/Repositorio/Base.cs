using DAL.Contexto;

namespace DAL.Repositorio
{
    public abstract class Base
    {
        protected ApplicationDbContext _applicationDbContext;

        public Base()
        {
            _applicationDbContext = new ApplicationDbContext();
        }

        public void Guardar()
        {
            _applicationDbContext.SaveChanges();
        }

    }
}