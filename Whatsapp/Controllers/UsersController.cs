using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<WhatsappUser> _userManager;
        public UsersController(ILogger<HomeController> logger, UserManager<WhatsappUser> userManager, ApplicationContext appcontext, IRepository<Users> users)
        {
            _logger = logger;
            _appcontext = appcontext;
            this._users = users;
            _userManager = userManager;


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

        public async Task<JsonResult> AddUser(Users user)
        {
            if(user != null)
            {
                var newUser = new WhatsappUser { UserName = user.Email, Email = user.Email, PhoneNumber = user.PhoneNumber, Name = user.Name, EmailConfirmed = true };
                var password = "Aaz@" + user.Name.Substring(4) + user.PhoneNumber.Substring(4) ;
                var result = await _userManager.CreateAsync(newUser, password);
                if (result.Succeeded)
                {
                    var res = await _userManager.AddToRoleAsync(newUser, user.Role);
                    _logger.LogInformation("User created a new account with password.");
                    return Json(res);
                }
            }
            return Json("ok");
        }

    }
}
