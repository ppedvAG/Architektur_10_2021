using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ppedv.Laureatus.Logic;
using ppedv.Laureatus.Model;
using ppedv.Laureatus.Model.Contracts;
using ppedv.Laureatus.UI.WebMVC.Models;

namespace ppedv.Laureatus.UI.WebMVC.Controllers
{
    public class PersonController : Controller
    {

        Core core = null;

        public PersonController(IRepository repo)
        {
            core = new Core(repo);
        }

        // GET: PersonController
        public ActionResult Index()
        {
            var alle = core.Repository.Query<Person>().OrderBy(x => x.Name);
            return View(alle);
        }

        public ActionResult Index2()
        {
            var alle = core.Repository.Query<Person>().OrderBy(x => x.Name);

            return View(alle.Select(p => new PersonViewModel(p)));
        }

        // GET: PersonController/Details/5
        public ActionResult Details(int id)
        {
            var p = core.Repository.GetById<Person>(id);
            return View(p);
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {
            return View(new Person() { Name = "NEU" });
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person person)
        {
            try
            {
                core.Repository.Add(person);
                core.Repository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(core.Repository.GetById<Person>(id));
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Person person)
        {
            try
            {
                core.Repository.Update(person);
                core.Repository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(core.Repository.GetById<Person>(id));
        }

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Person person)
        {
            try
            {
                core.Repository.Delete(person);
                core.Repository.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
