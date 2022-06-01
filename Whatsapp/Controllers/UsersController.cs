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
using System.Security.Claims;
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
    public class UsersController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private ApplicationContext _appcontext;
        private IRepository<Users> _users;
        private readonly UserManager<WhatsappUser> _userManager;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IEmailService _emailService;
       private readonly IMasterWebsiteService _masterWebsiteService;
        
        public UsersController(ILogger<HomeController> logger, UserManager<WhatsappUser> userManager, ApplicationContext appcontext, IRepository<Users> users, IUnitOfWorkFactory unitOfWorkFactory, IEmailService emailService, IMasterWebsiteService masterWebsiteService)
        {
            _logger = logger;
            _appcontext = appcontext;
            _users = users;
            _userManager = userManager;
            _unitOfWorkFactory = unitOfWorkFactory;
            _emailService = emailService;
            _masterWebsiteService = masterWebsiteService;
        }
        [Route("GetPackages")]
        public async Task<IActionResult> GetPackages()
        {
            var ms = new PackageService(_unitOfWorkFactory);
            PackageView packageView = await ms.GetPackageView() ?? new PackageView();
            return View(packageView);
        }
        [HttpPost]
        public IActionResult GetRaiseFundRequest(int Id)
        {
            UserFundRequest userFundRequest = new UserFundRequest()
            {
                UserId = Id
            };
            return PartialView("~/Views/Users/PartialView/_RaiseFundRequest.cshtml", userFundRequest);
        }
        [HttpPost]
        public async Task<IActionResult> AddRaiseFundRequest(UserFundRequest userFundRequest)
        {
            var us = new FundRequestService(_unitOfWorkFactory);
            userFundRequest.Status = "Pending";
            var data = await us.InsertFundRequest(userFundRequest);
            return Json(data);
        }

        [HttpPost]
        public IActionResult GetAddFund(int Id)
        {
            UserBalance userBalance = new UserBalance();
            return PartialView("~/Views/Users/PartialView/_AddFund.cshtml", userBalance);
        }
        [HttpPost]
        public async Task<IActionResult> AddFund(UserBalance userBalance)
        {
            userBalance.ModifyBy = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            userBalance.UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var us = new UserBalanceService(_unitOfWorkFactory);
            var data = await us.UpdateUserForAddFund(userBalance);
            return Json(data);
        }

    }
}
