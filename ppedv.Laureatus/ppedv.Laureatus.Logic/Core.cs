using ppedv.Laureatus.Model;
using ppedv.Laureatus.Model.Contracts;

namespace ppedv.Laureatus.Logic
{
    public class Core
    {
        public IRepository Repository { get; }

        public Core(IRepository repository)
        {
            Repository = repository;
        }

        public Person GetPersonWithMostWins()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                return new Person() { Name = "André" };

            return Repository.Query<Person>().OrderByDescending(x => x.Laureates.Count()).ThenBy(x => x.Name).FirstOrDefault();
        }


        public IEnumerable<Person> GetWinnersOfYear(int year)
        {
            return Repository.Query<Person>()
                             .Where(x => x.Laureates.Any(y => y.Price.Year == year))
                             .OrderBy(x => x.Name);
        }
    }
}