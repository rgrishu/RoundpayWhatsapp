using EmailService;
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
        private readonly IEmailService _emailService;
        public UsersController(ILogger<HomeController> logger, UserManager<WhatsappUser> userManager, ApplicationContext appcontext, IRepository<Users> users, IUnitOfWorkFactory unitOfWorkFactory, IEmailService emailService)
        {
            _logger = logger;
            _appcontext = appcontext;
            this._users = users;
            _userManager = userManager;
            _unitOfWorkFactory = unitOfWorkFactory;
            _emailService = emailService;
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

            var us = new UsersService(_unitOfWorkFactory);

            var list = await us.GetAllUsers();
            //  var list = await _appcontext.AspNetUsers.Select(x => new WhatsappUser{Name = x.Name,PhoneNumber=x.PhoneNumber,Email=x.Email}).ToListAsync();
            return PartialView("~/Views/Users/PartialView/_UsersList.cshtml", list);
        }

        public async Task<IActionResult> UserForm()
        {
            return PartialView("~/Views/Users/PartialView/_Registration.cshtml");
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
                if (user != null)
                {
                    var newUser = new WhatsappUser { UserName = user.Email, Email = user.Email, PhoneNumber = user.PhoneNumber, Name = user.Name, EmailConfirmed = true };
                    var password = "Aaz@" + user.Name.Substring(4) + user.PhoneNumber.Substring(4);
                    var result = await _userManager.CreateAsync(newUser, password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, user.Role);
                        #region SocialAlert
                        #region EmailSend

                        _emailService.Send(new EmailMessage
                        {
                            Content = "Test",
                            Subject = "Registration",
                            
                            FromAddresses = new List<EmailAddress> {
                            new EmailAddress{
                                    Address = "support@roundpay.in",
                                    Name = "",
                                }
                            },
                            ToAddresses = new List<EmailAddress> {
                            new EmailAddress{
                                    Address =user.Email,
                                    Name = user.Name,
                                }
                            }
                        });



                        //EmailService es = new EmailService(_unitOfWorkFactory);
                        //var emailreq = new AlertReplacementModel()
                        //{ };
                        //es.RegistrationEmail(emailreq);
                        #endregion


                        #region SMSSend
                        //SMSService ss = new SMSService(_unitOfWorkFactory);
                        //var smsreq = new AlertReplacementModel()
                        //{ };
                        //ss.RegistrationSMS(smsreq);
                        #endregion


                        #region WhatsappSend
                        WhatsappService ws = new WhatsappService(_unitOfWorkFactory);
                        var wc = new WhatsappConversation()
                        {
                            ContactId = user.PhoneNumber.Length == 10 ? "91" + user.PhoneNumber : user.PhoneNumber,
                            SenderName = user.Name,
                            Text = $"Welcome Your UserID Is { user.Email} and Password IS {password} ",
                            Type = "Text"
                        };
                        await ws.WhatsappAlertHub(wc);
                        #endregion
                        #endregion

                        _logger.LogInformation("User created a new account with password.");
                        res.StatusCode = (int)ResponseStatus.Success;
                        res.ResponseText = "Registration Successfull.";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(res);
        }

    }
}
