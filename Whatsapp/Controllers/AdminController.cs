using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
    [Authorize]
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
                res = await ms.UpdateUserFundRequest(userFundRequest, LoggedInUserId, Status);
            }
            return Json(res);
        }
    }
}
