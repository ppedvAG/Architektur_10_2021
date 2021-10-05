using ppedv.Laureatus.Model;
using ppedv.Laureatus.Model.Contracts;

namespace ppedv.Laureatus.Data.EfCore
{
    public class EfRepository : IRepository
    {
        EfContext _context = new EfContext();

        public void Add<T>(T entity) where T : Entity
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            _context.Set<T>().Remove(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            return _context.Set<T>();
        }

        public T GetById<T>(int id) where T : Entity
        {
            return _context.Set<T>().Find(id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            _context.Set<T>().Update(entity);
        }
    }
}
