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
using WAEFCore22.AppCode.BusinessLogic;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.Interface;
using Whatsapp.Models;
using Whatsapp.Models.Data;
using Whatsapp.Models.UtilityModel;
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
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public UsersController(ILogger<HomeController> logger, UserManager<WhatsappUser> userManager, ApplicationContext appcontext, IRepository<Users> users, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _logger = logger;
            _appcontext = appcontext;
            _users = users;
            _userManager = userManager;
            _unitOfWorkFactory = unitOfWorkFactory;

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
            var us = new UsersService(_unitOfWorkFactory, _userManager, _appcontext);
            var list = await us.GetAllUsers();
            return PartialView("~/Views/Users/PartialView/_UsersList.cshtml", list);
        }

        public async Task<IActionResult> UserForm(int id)
        {
            Users users = null;
            try
            {
                if (id != 0)
                {
                    var us = new UsersService(_unitOfWorkFactory, _userManager, _appcontext);
                    var list = await us.GetAllUsersById(id);
                    WhatsappUser whatsapp = list.FirstOrDefault();
                    users = new Users();
                    users.Id = whatsapp.Id;
                    users.Name = whatsapp.Name;
                    users.Email = whatsapp.Email;
                    users.PhoneNumber = whatsapp.PhoneNumber;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return PartialView("~/Views/Users/PartialView/_Registration.cshtml", users);
        }

        public async Task<JsonResult> AddUser(Users user)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            try
            {
                if (user.Id == 0)
                {
                    var us = new UsersService(_unitOfWorkFactory, _userManager, _appcontext);
                    res = await us.AddUser(user);
                    _logger.LogInformation("User created a new account with password.");
                }
                else
                {
                    var us = new UsersService(_unitOfWorkFactory, _userManager, _appcontext);
                    res = await us.UpdateUsers(user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            if (id != 0)
            {
                var us = new UsersService(_unitOfWorkFactory, _userManager, _appcontext);
                res = await us.Delete(id);
            }
            return Json(res);
        }

    }
}
