using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WAEFCore22.AppCode.BusinessLogic;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.AppCode.BusinessLogic;
using Whatsapp.AppCode.Extensions;
using Whatsapp.Interface;
using Whatsapp.Models;
using Whatsapp.Models.Data;
using Whatsapp.Models.UtilityModel;
using Whatsapp.Models.ViewModel;
using Whatsapp.Services.Interface;

namespace Whatsapp.Controllers
{
    [Authorize]
    public class UserManagementController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext _appcontext;
        private IRepository<Users> _users;
        private readonly UserManager<WhatsappUser> _userManager;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IEmailService _emailService;
        private readonly IMasterWebsiteService _masterWebsiteService;

        public UserManagementController(ILogger<HomeController> logger, UserManager<WhatsappUser> userManager, ApplicationContext appcontext, IRepository<Users> users, IUnitOfWorkFactory unitOfWorkFactory, IEmailService emailService, IMasterWebsiteService masterWebsiteService)
        {
            _logger = logger;
            _appcontext = appcontext;
            _users = users;
            _userManager = userManager;
            _unitOfWorkFactory = unitOfWorkFactory;
            _emailService = emailService;
            _masterWebsiteService = masterWebsiteService;
        }
        [Route("UsersList")]
        [HttpGet]
        public IActionResult UsersList()
        {
            var WID = User.GetLoggedInWID();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetUsersListAsync()
        {

            var us = new UsersService(_unitOfWorkFactory, _userManager, _appcontext);

            var list = await us.GetAllUsers();
            //  var list = await _appcontext.AspNetUsers.Select(x => new WhatsappUser{Name = x.Name,PhoneNumber=x.PhoneNumber,Email=x.Email}).ToListAsync();
            return PartialView("~/Views/UserManagement/PartialView/_UsersList.cshtml", list);
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
                    var rolename = await _userManager.GetRolesAsync(whatsapp);
                    users = new Users();
                    users.Id = whatsapp.Id;
                    users.Name = whatsapp.Name;
                    users.Email = whatsapp.Email;
                    users.PhoneNumber = whatsapp.PhoneNumber;
                    users.Role = rolename[0].ToString().ToLower();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return PartialView("~/Views/UserManagement/PartialView/_Registration.cshtml", users);
        }

        public async Task<JsonResult> AddUser(Users user)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = ResponseStatus.Failed.ToString(),
            };
            try
            {
                if (user.Id == 0)
                {
                    var WID = User.GetLoggedInWID();
                    var newUser = new WhatsappUser { UserName = user.Email, Email = user.Email, PhoneNumber = user.PhoneNumber, Name = user.Name, EmailConfirmed = true, WID = WID };
                    var password = "Aaz@" + user.Name.Substring(4) + user.PhoneNumber.Substring(4);
                    var result = await _userManager.CreateAsync(newUser, password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, user.Role);
                        var userId = User.GetLoggedInUserId<int>();
                        var userBalanceResult = InsertInUserBalance(newUser.Id);


                        MasterServices ms = new MasterServices(_unitOfWorkFactory);
                        var cp = _masterWebsiteService.masterWebsite().Result;
                        var mt = ms.GetMessageTemplate().Result;
                        var param = new AlertReplacementModel()
                        {
                            UserMobileNo = user.PhoneNumber,
                            Password = password,
                            PinPassword = password,
                            CommonStr = user.Email,
                            EmailID = user.Email,
                            UserName = user.Name,
                            Company = cp.ComapnyName,
                            CompanyDomain = cp.WebsiteName,
                            Subject = "Registration"
                        };
                        #region SocialAlert
                        #region EmailSend
                        EmailServices es = new EmailServices(_unitOfWorkFactory, _emailService);
                        await es.SendMail(param, mt.EmailTemplate);
                        #endregion
                        #region SMSSend
                        SMSService ss = new SMSService(_unitOfWorkFactory);
                        await ss.RegistrationSMS(param);
                        #endregion
                        #region WhatsappSend
                        WhatsappService ws = new WhatsappService(_unitOfWorkFactory);
                        await ws.WhatsappAlertHub(param, mt.WhatsappTemplate);
                        #endregion
                        #endregion

                        _logger.LogInformation("User created a new account with password.");
                        res.StatusCode = (int)ResponseStatus.Success;
                        res.ResponseText = "Registration Successfull.";
                    }
                }
                else
                {
                    var ms = new UsersService(_unitOfWorkFactory, _userManager, _appcontext);
                    res = await ms.UpdateUsers(user);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            if (id != 0)
            {
                var ms = new UsersService(_unitOfWorkFactory, _userManager, _appcontext);
                res = await ms.Delete(id);
            }
            return Json(res);
        }
        public Response InsertInUserBalance(int Id)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Success,
                ResponseText = "Successfull"
            };
            try
            {
                UserBalance userBalance = new UserBalance()
                {
                    UserId = Id,
                    Balance = 0,
                    CreatedDate = DateTime.Now,
                    ModifyBy = User.Identity.Name
                };
                var rest = _appcontext.Add(userBalance);
                _appcontext.SaveChangesAsync();
                //var ms = new UserBalanceService(_unitOfWorkFactory);
                //res = await ms.InsertUserBalance(userBalance);
            }
            catch
            {
                throw;
            }
            return res;
        }
    }
}
