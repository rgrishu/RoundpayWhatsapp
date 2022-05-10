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
using Whatsapp.Models.ViewModel;

namespace Whatsapp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private ApplicationContext appcontext;
        private IRepository<Users> _users;
        public UsersController(ILogger<HomeController> logger, ApplicationContext appcontext, IRepository<Users> users)
        {
            _logger = logger;
            appcontext = appcontext;
            this._users = users;
            
        }
        public IActionResult UsersList()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetUsersList()
        {
            return PartialView("~/Views/Users/PartialView/_UsersList.cshtml", _users.GetAll());
        }
    }
}
