using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WAEFCore22.AppCode.BusinessLogic;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.AppCode.BusinessLogic;
using Whatsapp.Interface;
using Whatsapp.Models;
using Whatsapp.Models.Data;
using Whatsapp.Models.UtilityModel;
using Whatsapp.Models.ViewModel;

namespace Whatsapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext _appcontext;
        private readonly UserManager<WhatsappUser> _userManager;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public AdminController(ILogger<HomeController> logger, UserManager<WhatsappUser> userManager, ApplicationContext appcontext, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _logger = logger;
            _appcontext = appcontext;
            _unitOfWorkFactory = unitOfWorkFactory;
            _userManager = userManager;
        }
        [Route("FundRequestList")]
        public IActionResult FundRequestList()
        {
            ViewData["Title"] = "Master Service";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetFundRequestList()
        {
            var ms = new FundRequestService(_unitOfWorkFactory);
            IEnumerable<UserFundRequest> data = await ms.GetAll();
            return PartialView("~/Views/Admin/PartialView/_GetFundRequestList.cshtml", data);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFundRequest(int id, string Status)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            if (ModelState.IsValid)
            {
                UserFundRequest userFundRequest = new UserFundRequest() { Id = id };
                var ms = new FundRequestService(_unitOfWorkFactory);
                var LoggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var LoggedInUserName = User.FindFirstValue(ClaimTypes.Name);
                res = await ms.UpdateUserFundRequest(userFundRequest, LoggedInUserId, Status);
            }
            return Json(res);
        }
        [Route("WabaNumbers")]
        [HttpGet]
        public IActionResult WabaNumbers()
        {
            ViewData["Title"] = "WABAs Number";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> WabaNumbersList()
        {
            var ms = new WABAsNumberService(_unitOfWorkFactory);
            IEnumerable<WABAsNumber> data = await ms.GetAll();
            return PartialView("~/Views/Admin/PartialView/_WabaNumbersList.cshtml", data);
        }
        public async Task<IActionResult> CreateNumbers(int? id)
        {
            WABAsNumber mf = null;
            if (id != 0)
            {
                mf = await _appcontext.WABAsNumbers
                    .Where(h => h.Id == id)
                    .FirstOrDefaultAsync();
            }
            ViewData["Providers"] = new SelectList(_appcontext.WABAsProviders, "Id", "APICode");
            return PartialView("~/Views/Admin/PartialView/_AddNumbers.cshtml", mf ?? new WABAsNumber());
        }

        [HttpPost]
        public async Task<IActionResult> AddNumbers(WABAsNumber mp)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            var ms = new WABAsNumberService(_unitOfWorkFactory);
            if (ModelState.IsValid)
            {
                if (mp.Id == 0)
                {
                    mp.CreatedDate = DateTime.Now;
                    res = await ms.InsertWABAsNumber(mp);
                }
                else
                {
                    mp.ModifiedDate = DateTime.Now;
                    res = await ms.UpdateWABAsNumber(mp);
                }
            }
            return Json(res);
        }
    }
}
