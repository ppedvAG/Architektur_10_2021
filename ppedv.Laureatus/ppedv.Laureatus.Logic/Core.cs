using ppedv.Laureatus.Data.EfCore;
using ppedv.Laureatus.Model;

namespace ppedv.Laureatus.Logic
{
    public class Core
    {
        public IEnumerable<Person> GetWinnersOfYear(int year)
        {
            var con = new EfContext();
            return con.Persons.Where(x => x.Laureates.Any(y => y.Price.Year == year))
                              .OrderBy(x => x.Name);
        }
    }
}