using ppedv.Laureatus.Model;
using ppedv.Laureatus.Model.Contracts;
using System.Linq;

namespace ppedv.Laureatus.Logic.Tests
{
    public class TestRepo : IRepository
    {
        public void Add<T>(T entity) where T : Model.Entity
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T entity) where T : Model.Entity
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public T GetById<T>(int id) where T : Model.Entity
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<T> Query<T>() where T : Model.Entity
        {
            if(typeof(T) == typeof(Person))
            {
                var p1 = new Person() { Name = "Barney" };
                p1.Laureates.Add(new Laureate());

                var p2 = new Person() { Name = "Fred" };
                p2.Laureates.Add(new Laureate());
                p2.Laureates.Add(new Laureate());

                return new[] { p1, p2 }.Cast<T>().AsQueryable();
            }

            throw new System.NotImplementedException();
        }

        public int Save()
        {
            throw new System.NotImplementedException();
        }

        public void Update<T>(T entity) where T : Model.Entity
        {
            throw new System.NotImplementedException();
        }
    }
}