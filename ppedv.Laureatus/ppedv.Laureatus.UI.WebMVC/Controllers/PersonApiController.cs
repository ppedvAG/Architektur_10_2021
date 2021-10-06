using Microsoft.AspNetCore.Mvc;
using ppedv.Laureatus.Logic;
using ppedv.Laureatus.Model;
using ppedv.Laureatus.Model.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ppedv.Laureatus.UI.WebMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonApiController : ControllerBase
    {
        Core core = null;

        public PersonApiController(IRepository repo)
        {
            core = new Core(repo);
        }


        // GET: api/<PersonApiController>
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return core.Repository.Query<Person>().ToList();
        }

        // GET api/<PersonApiController>/5
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return core.Repository.GetById<Person>(id);
        }

        // POST api/<PersonApiController>
        [HttpPost]
        public void Post([FromBody] Person p)
        {
            core.Repository.Add(p);
            core.Repository.Save();
        }

        // PUT api/<PersonApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Person p)
        {
            core.Repository.Update<Person>(p);
            core.Repository.Save();
        }

        // DELETE api/<PersonApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            core.Repository.Update<Person>(Get(id));
            core.Repository.Save();
        }
    }
}
