using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private ApplicationContext _appcontext;
        private IRepository<Users> _users;
        public UsersController(ILogger<HomeController> logger, ApplicationContext appcontext, IRepository<Users> users)
        {
            _logger = logger;
            _appcontext = appcontext;
            this._users = users;
            
        }
        [Route("UsersList")]
        [HttpGet]
        public IActionResult UsersList()
        {
            return View();
        }
      
      
        [HttpPost]
        public async Task<IActionResult> GetUsersListAsync()
        {
            var list = await _appcontext.AspNetUsers.Select(x => new WhatsappUser{Name = x.Name,PhoneNumber=x.PhoneNumber,Email=x.Email}).ToListAsync();
            return PartialView("~/Views/Users/PartialView/_UsersList.cshtml", list);
        }
       
        public async Task<IActionResult> UserForm()
        {
            return PartialView("~/Views/Users/PartialView/_Registration.cshtml");
        }

        public async Task<IActionResult> AddUser(Users users)
        {






            return PartialView("~/Views/Users/PartialView/_Registration.cshtml");
        }

    }
}
