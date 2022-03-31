using AjaxPeople.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AjaxPeople.Data;

namespace AjaxPeople.Web.Controllers
{
    public class HomeController : Controller
    {

        private string _connectionString = @"Data Source =.\sqlexpress;Initial Catalog=PeopleAndCars;IntegratedSecurity=true;";
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var manager = new PeopleManager(_connectionString);
            List<Person> people = manager.GetAllPeople();
            return Json(people);
        }
        [HttpPost]
        public IActionResult AddPerson (Person person)
        {
            var manager = new PeopleManager(_connectionString);
            manager.AddPerson(person);
            return Json(person);
        }

        [HttpPost]
        public IActionResult EditPerson (Person person)
        {
            var manager = new PeopleManager(_connectionString);
            manager.Edit(person);
            return Json(person);
        }

        [HttpPost]
        public IActionResult DeletePerson (int id)
        {
            var manager = new PeopleManager(_connectionString);
            manager.Delete(id);
            return Json(id);
        }
        
    }
}
