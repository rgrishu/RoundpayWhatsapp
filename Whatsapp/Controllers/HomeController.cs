using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Whatsapp.Interface;
using Whatsapp.Models;
using Whatsapp.Models.Data;

namespace Whatsapp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext appcontext;
        private IRepository<State> StateRep;
        private IRepository<City> CityRep;


        public HomeController(ILogger<HomeController> logger, ApplicationContext appcontext, IRepository<State> state, IRepository<City> city)
        {

            _logger = logger;
            appcontext = appcontext;
            this.StateRep = state;
            this.CityRep = city;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult State()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddState(State st)
        {
            StateRep.Insert(st);
            return View("State");
        }
        [HttpGet]
        public IActionResult City()
        {
            return View(StateRep.GetAll());
        }

        [HttpPost]
        public IActionResult AddCity(City ct)
        {
            CityRep.Insert(ct);
            return View("City", StateRep.GetAll());
        }


        [HttpGet]
        public IActionResult GetStateCity()
        {
            return View(CityRep.GetAll());
        }
        

    }
}
